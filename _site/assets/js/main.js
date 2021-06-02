// @ts-check
(function () {
  'use strict';

  const stravaCardBody = document.querySelector('.strava-card .card-body');
  let timeoutId;

  if (stravaCardBody) {
   
  }

  if (isDateTimeFormatSupported()) {
    formatDates();
  }

  ////////////////

  function formatDates() {
    const language = navigator.language;

    const dateTimeOptions = { month: 'long', year: 'numeric', day: 'numeric' };
    const dateTimeFormat = new Intl.DateTimeFormat(language, dateTimeOptions);

    const relativeTimeOptions = { numeric: 'auto' };
    // @ts-ignore
    const relativeTimeFormat = new Intl.RelativeTimeFormat(language, relativeTimeOptions);

    const oneDayInMs = 24 * 60 * 60 * 1000;
    const thirtyDaysInMs = 30 * oneDayInMs;

    const publishDates = [...document.querySelectorAll('.publish-date time')];

    publishDates.forEach(date => {
      const timeSincePublishInMs = new Date().getTime() - new Date(date.textContent).getTime();

      if ((timeSincePublishInMs > thirtyDaysInMs) || !isRelativeTimeFormatSupported()) {
        date.textContent = dateTimeFormat.format(new Date(date.textContent));
      } else {
        const daysSincePublish = Math.floor(timeSincePublishInMs / oneDayInMs);

        date.textContent = relativeTimeFormat.format(-daysSincePublish, 'day');
      }
    });
  }

  function isDateTimeFormatSupported() {
    return typeof Intl.DateTimeFormat === 'function';
  }

  function isRelativeTimeFormatSupported() {
    // @ts-ignore
    return typeof Intl.RelativeTimeFormat === 'function';
  }

  /**
   * @param {number} distance
   */
  function toKm(distance) {
    return (distance / 1000).toFixed(2);
  }

  /**
   * @param {number} totalTimeInSeconds
   */
  function formatTime(totalTimeInSeconds) {
    const oneHourInSeconds = 3600;
    let hours = Math.floor(totalTimeInSeconds / oneHourInSeconds);
    let minutes = Math.floor((totalTimeInSeconds - (hours * oneHourInSeconds)) / 60);
    let seconds = totalTimeInSeconds - (hours * oneHourInSeconds) - (minutes * 60);

    if (minutes < 10) {
      // @ts-ignore
      minutes = `0${minutes}`;
    }
    
    if (seconds < 10) {
      // @ts-ignore
      seconds = `0${seconds}`;
    }

    if (hours > 0) {
      return `${hours}:${minutes}:${seconds}`;
    }

    return `${minutes}:${seconds}`;
  }
      

  function showStartFetchingMessage() {
    stravaCardBody.innerHTML = '<p><small>🏃 Remembering Thoughts...</small></p>';
  }
}());
