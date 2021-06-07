---
layout: post
category: posts
title: "F# Canopy UI tests -- http://lefthandedgoat.github.io/canopy/"
date: "2013-05-14"
tags: code fsharp
---
`
   # FSharp Canopy Tests
```

    namespace RegressionTests

    open System
    open System.Configuration
    open runner
    open canopy
    open configuration
    open reporters
    open QuoteHelper

    module QuoteSiteTests =

        let runTest = (

            start chrome
            let mainBrowser = browser
            elementTimeout <- 15.0
            compareTimeout <- 15.0

            let _testpage =             ConfigurationManager.AppSettings.\["AolSiteUrl"]
            let _username =             ConfigurationManager.AppSettings.\["AolChallengerId"]
            let _password =             ConfigurationManager.AppSettings.\["AolPassword"]
            let _passwordChangeToThis = ConfigurationManager.AppSettings.\["AolPasswordChangeToThis"]
            let _includePasswordChangeTest = ConfigurationManager.AppSettings.\["IncludePasswordChangeTest"]
            let _quickSearchText = ConfigurationManager.AppSettings.\["QuickSearchText"]
            let _quickSearchFindInvestor = ConfigurationManager.AppSettings.\["QuickSearchFindInvestor"]
            let _path = ConfigurationManager.AppSettings.\["OutputTestResults"]

            QuoteHelper.deleteFiles _path "" true

            for file in System.IO.Directory.EnumerateFiles(_path) do
                let tempPath = System.IO.Path.Combine(_path, file)
                System.IO.File.Delete(tempPath)

            context ("Testing :: eQuote - " + _testpage + "/Quote")

            before (fun _ -> 
                  describe "Starting test"
                  url (_testpage + "/Quote")
                )
           
            ntest "Login" (fun _ ->
                describe ("Login with " + _username)
                url _testpage
                "#userId" << _username
                "#password" <<  _password
                click ".submit"
              //  on (_testpage + "/Secure/Home/")
            )

            ntest "GIP Quote - Fixed Term" (fun _ ->
                    describe "Creating GIP quote – fixed 3 year"
                    js "$('input:radio\[name=NewPolicyType]:first').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    let setQuote = QuoteHelper.createQuoteTerm "100000" "Monthly" "3"
                    js setQuote |> ignore
                    click "#btnRunQuote"
                    sleep 5
                    click "#btnSaveQuote"
                    sleep 3
                    let linkText = read "#yourQuoteId"
                    describe linkText
                    screenshot _path linkText |> ignore
                    )

            ntest "GIP quote – life" (fun _ ->
                    describe "Creating GIP quote – life"
                    js "$('input:radio\[name=NewPolicyType]::nth(1)').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    js " $('input:radio\[name=ProductTerm]::nth(1)').click();" |> ignore
                    sleep 1
                    let setQuote = QuoteHelper.createQuoteLife "100000" "Monthly" 
                    js setQuote |> ignore
                    sleep 1
                    js "$('#InitialCommissionRate').val('0');" |> ignore
                    click "#btnRunQuote"
                    sleep 5
                    click "#btnSaveQuote"
                    sleep 3
                    let linkText = read "#yourQuoteId"
                    describe linkText
                    screenshot _path linkText |> ignore
                    )

            ntest "GA Quote - Fixed Term" (fun _ ->
                    describe "Creating Fee for service annuity quote - FIXED"
                    js "$('input:radio\[name=NewPolicyType]:first').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    let setQuote = QuoteHelper.createQuoteTerm "100000" "Monthly" "3"
                    js setQuote |> ignore
                    sleep 1
                    js "$('#InitialCommissionRate').val('0.0055');" |> ignore
                    click "#btnRunQuote"
                    sleep 5
                    click "#btnSaveQuote"
                    sleep 3
                    let linkText = read "#yourQuoteId"
                    describe linkText
                    screenshot _path linkText |> ignore
                    )

            ntest "GA quote – life" (fun _ ->
                    describe "Creating GA quote – life"
                    js "$('input:radio\[name=NewPolicyType]::nth(1)').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    js " $('input:radio\[name=ProductTerm]::nth(1)').click();" |> ignore
                    sleep 1
                    let setQuote = QuoteHelper.createQuoteLife "100000" "Yearly" 
                    js setQuote |> ignore
                    sleep 1
                    js "$('#InitialCommissionRate').val('0');" |> ignore
                    click "#btnRunQuote"
                    sleep 5
                    click "#btnSaveQuote"
                    sleep 3
                    let linkText = read "#yourQuoteId"
                    describe linkText
                    screenshot _path linkText |> ignore
                    )

            ntest "GreenId Test" (fun _ ->
                    describe "GreenId Test"
                    js "$('input:radio\[name=NewPolicyType]::nth(1)').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    js " $('input:radio\[name=ProductTerm]::nth(1)').click();" |> ignore
                    sleep 1
                    let setQuote = QuoteHelper.createQuoteLife "100000" "Yearly" 
                    js setQuote |> ignore
                    sleep 1
                    js "$('#InitialCommissionRate').val('0');" |> ignore
                    click "#btnRunQuote"
                    sleep 5
                    click "#btnSaveQuote"
                    sleep 3
                    let linkText = read "#yourQuoteId"
                    describe linkText
                    click "#btnApply"
                    sleep 5
                    js QuoteHelper.greenId |> ignore
                    sleep 1
                    click "#InvestorDetails_IdentityVerification_CheckId"
                    screenshot _path ("GreenId -" + linkText )|> ignore
                    )

            run ()
            quit mainBrowser

    )

namespace RegressionTests

    module QuoteHelper =

        let createQuoteTerm amount paymentFrequency term = (
                                             @"  $('#InvestorDetails_Title').val('Mr');
                                            $('#InvestorDetails_GivenNames').val('test');
                                            $('#InvestorDetails_Surname').val('client');
                                            $('#InvestorDetails_DateOfBirth_day').val(1);
                                            $('#InvestorDetails_DateOfBirth_month').val(1);
                                            $('#InvestorDetails_DateOfBirth_year').val(1937);
                                            $('#InvestmentAmount').val(" + amount + ");
                                            $('#InvestmentAmount_editor').val('$100,000.00');
                                            $('#PaymentFrequency').val('" + paymentFrequency + "');
                                            $('#Term').val('" + term + "');
                                            $('#InitialCommissionRate').val('0');
                                            $('#Indexation').val('ZeroPercent');
                                            $('#OngoingCommissionRate').val('0.00550');
                                            $('#ResidualCapitalValueRate').val('1.00000');
                                            $('#JointInvestorDetails_Title').val('Mr');
                                            $('#JointInvestorDetails_GivenNames').val('joint test');
                                            $('#JointInvestorDetails_Surname').val('client');
                                            $('#JointInvestorDetails_DateOfBirth_day').val(31);
                                            $('#JointInvestorDetails_DateOfBirth_month').val(12);
                                            $('#JointInvestorDetails_DateOfBirth_year').val(1956);")

        let createQuoteLife amount paymentFrequency  = (
                                             @"  $('#InvestorDetails_Title').val('Mr');
                                            $('#InvestorDetails_GivenNames').val('test');
                                            $('#InvestorDetails_Surname').val('client');
                                            $('#InvestorDetails_DateOfBirth_day').val(1);
                                            $('#InvestorDetails_DateOfBirth_month').val(1);
                                            $('#InvestorDetails_DateOfBirth_year').val(1937);
                                            $('#InvestmentAmount').val(" + amount + ");
                                            $('#InvestmentAmount_editor').val('$100,000.00');
                                            $('#PaymentFrequency').val('" + paymentFrequency + "');
                                            $('#InitialCommissionRate').val('0.0055');
                                            $('#Indexation').val('ZeroPercent');
                                            $('#OngoingCommissionRate').val('0.00550');
                                            $('#ResidualCapitalValueRate').val('1.00000');
                                            $('#JointInvestorDetails_Title').val('Mr');
                                            $('#JointInvestorDetails_GivenNames').val('joint test');
                                            $('#JointInvestorDetails_Surname').val('client');
                                            $('#JointInvestorDetails_DateOfBirth_day').val(31);
                                            $('#JointInvestorDetails_DateOfBirth_month').val(12);
                                            $('#JointInvestorDetails_DateOfBirth_year').val(1956);")

        let greenId = (@"
                        $('#InvestorDetails_ResidentialAddress_UnitNumber').val('5');
                        $('#InvestorDetails_ResidentialAddress_StreetNumber').val('48');
                        $('#InvestorDetails_ResidentialAddress_StreetName').val('Manchester');
                        $('#InvestorDetails_ResidentialAddress_Suburb').val('Gymea');
                        $('#InvestorDetails_ResidentialAddress_StreetType').val('RD');
                        $('#InvestorDetails_ResidentialAddress_State').val('NSW');
                        $('#InvestorDetails_ResidentialAddress_Postcode').val(2227);
                    
        ")

        let rec deleteFiles srcPath pattern includeSubDirs =
    
            if not <| System.IO.Directory.Exists(srcPath) then
                let msg = System.String.Format("Source directory does not exist or could not be found: {0}", srcPath)
                raise (System.IO.DirectoryNotFoundException(msg))

                for file in System.IO.Directory.EnumerateFiles(srcPath, pattern) do
                let tempPath = System.IO.Path.Combine(srcPath, file)
                System.IO.File.Delete(tempPath)

                if includeSubDirs then
                let srcDir = new System.IO.DirectoryInfo(srcPath)
                for subdir in srcDir.GetDirectories() do
                deleteFiles subdir.FullName pattern includeSubDirs

`           

```