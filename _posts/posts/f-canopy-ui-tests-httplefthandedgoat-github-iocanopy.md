---
layout: post
category: posts
title: "F# Canopy UI tests -- http://lefthandedgoat.github.io/canopy/"
date: "2013-05-14"
categories: 
  - "net"
  - "f"
  - "tdd-bdd"
  - "tools"
---

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

            let \_testpage =             ConfigurationManager.AppSettings.\["AolSiteUrl"\]
            let \_username =             ConfigurationManager.AppSettings.\["AolChallengerId"\]
            let \_password =             ConfigurationManager.AppSettings.\["AolPassword"\]
            let \_passwordChangeToThis = ConfigurationManager.AppSettings.\["AolPasswordChangeToThis"\]
            let \_includePasswordChangeTest = ConfigurationManager.AppSettings.\["IncludePasswordChangeTest"\]
            let \_quickSearchText = ConfigurationManager.AppSettings.\["QuickSearchText"\]
            let \_quickSearchFindInvestor = ConfigurationManager.AppSettings.\["QuickSearchFindInvestor"\]
            let \_path = ConfigurationManager.AppSettings.\["OutputTestResults"\]

            QuoteHelper.deleteFiles \_path "" true

            for file in System.IO.Directory.EnumerateFiles(\_path) do
                let tempPath = System.IO.Path.Combine(\_path, file)
                System.IO.File.Delete(tempPath)

            context ("Testing :: eQuote - " + \_testpage + "/Quote")

            before (fun \_ -> 
                  describe "Starting test"
                  url (\_testpage + "/Quote")
                )
           
            ntest "Login" (fun \_ ->
                describe ("Login with " + \_username)
                url \_testpage
                "#userId" << \_username
                "#password" <<  \_password
                click ".submit"
              //  on (\_testpage + "/Secure/Home/")
            )

            ntest "GIP Quote - Fixed Term" (fun \_ ->
                    describe "Creating GIP quote – fixed 3 year"
                    js "$('input:radio\[name=NewPolicyType\]:first').attr('checked', true);" |> ignore
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
                    screenshot \_path linkText |> ignore
                    )

            ntest "GIP quote – life" (fun \_ ->
                    describe "Creating GIP quote – life"
                    js "$('input:radio\[name=NewPolicyType\]::nth(1)').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    js " $('input:radio\[name=ProductTerm\]::nth(1)').click();" |> ignore
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
                    screenshot \_path linkText |> ignore
                    )

            ntest "GA Quote - Fixed Term" (fun \_ ->
                    describe "Creating Fee for service annuity quote - FIXED"
                    js "$('input:radio\[name=NewPolicyType\]:first').attr('checked', true);" |> ignore
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
                    screenshot \_path linkText |> ignore
                    )

            ntest "GA quote – life" (fun \_ ->
                    describe "Creating GA quote – life"
                    js "$('input:radio\[name=NewPolicyType\]::nth(1)').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    js " $('input:radio\[name=ProductTerm\]::nth(1)').click();" |> ignore
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
                    screenshot \_path linkText |> ignore
                    )

            ntest "GreenId Test" (fun \_ ->
                    describe "GreenId Test"
                    js "$('input:radio\[name=NewPolicyType\]::nth(1)').attr('checked', true);" |> ignore
                    click "#btnContinue1"
                    sleep 5
                    js " $('input:radio\[name=ProductTerm\]::nth(1)').click();" |> ignore
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
                    click "#InvestorDetails\_IdentityVerification\_CheckId"
                    screenshot \_path ("GreenId -" + linkText )|> ignore
                    )

            run ()
            quit mainBrowser

    )

namespace RegressionTests

    module QuoteHelper =

        let createQuoteTerm amount paymentFrequency term = (
                                             @"  $('#InvestorDetails\_Title').val('Mr');
                                            $('#InvestorDetails\_GivenNames').val('test');
                                            $('#InvestorDetails\_Surname').val('client');
                                            $('#InvestorDetails\_DateOfBirth\_day').val(1);
                                            $('#InvestorDetails\_DateOfBirth\_month').val(1);
                                            $('#InvestorDetails\_DateOfBirth\_year').val(1937);
                                            $('#InvestmentAmount').val(" + amount + ");
                                            $('#InvestmentAmount\_editor').val('$100,000.00');
                                            $('#PaymentFrequency').val('" + paymentFrequency + "');
                                            $('#Term').val('" + term + "');
                                            $('#InitialCommissionRate').val('0');
                                            $('#Indexation').val('ZeroPercent');
                                            $('#OngoingCommissionRate').val('0.00550');
                                            $('#ResidualCapitalValueRate').val('1.00000');
                                            $('#JointInvestorDetails\_Title').val('Mr');
                                            $('#JointInvestorDetails\_GivenNames').val('joint test');
                                            $('#JointInvestorDetails\_Surname').val('client');
                                            $('#JointInvestorDetails\_DateOfBirth\_day').val(31);
                                            $('#JointInvestorDetails\_DateOfBirth\_month').val(12);
                                            $('#JointInvestorDetails\_DateOfBirth\_year').val(1956);")

        let createQuoteLife amount paymentFrequency  = (
                                             @"  $('#InvestorDetails\_Title').val('Mr');
                                            $('#InvestorDetails\_GivenNames').val('test');
                                            $('#InvestorDetails\_Surname').val('client');
                                            $('#InvestorDetails\_DateOfBirth\_day').val(1);
                                            $('#InvestorDetails\_DateOfBirth\_month').val(1);
                                            $('#InvestorDetails\_DateOfBirth\_year').val(1937);
                                            $('#InvestmentAmount').val(" + amount + ");
                                            $('#InvestmentAmount\_editor').val('$100,000.00');
                                            $('#PaymentFrequency').val('" + paymentFrequency + "');
                                            $('#InitialCommissionRate').val('0.0055');
                                            $('#Indexation').val('ZeroPercent');
                                            $('#OngoingCommissionRate').val('0.00550');
                                            $('#ResidualCapitalValueRate').val('1.00000');
                                            $('#JointInvestorDetails\_Title').val('Mr');
                                            $('#JointInvestorDetails\_GivenNames').val('joint test');
                                            $('#JointInvestorDetails\_Surname').val('client');
                                            $('#JointInvestorDetails\_DateOfBirth\_day').val(31);
                                            $('#JointInvestorDetails\_DateOfBirth\_month').val(12);
                                            $('#JointInvestorDetails\_DateOfBirth\_year').val(1956);")

        let greenId = (@"
                        $('#InvestorDetails\_ResidentialAddress\_UnitNumber').val('5');
                        $('#InvestorDetails\_ResidentialAddress\_StreetNumber').val('48');
                        $('#InvestorDetails\_ResidentialAddress\_StreetName').val('Manchester');
                        $('#InvestorDetails\_ResidentialAddress\_Suburb').val('Gymea');
                        $('#InvestorDetails\_ResidentialAddress\_StreetType').val('RD');
                        $('#InvestorDetails\_ResidentialAddress\_State').val('NSW');
                        $('#InvestorDetails\_ResidentialAddress\_Postcode').val(2227);
                    
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

           

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }<br /> .csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }<br />
