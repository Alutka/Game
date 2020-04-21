(function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
console.log("index log dupa");
require("./notificactions/DisplayNotification");
require("./progress bars/ProgressBars");
require("./info/info");

},{"./info/info":2,"./notificactions/DisplayNotification":3,"./progress bars/ProgressBars":4}],2:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const infoContainer = document.getElementById("info");
const nameContainer = document.createElement("div");
nameContainer.id = "name_container";
infoContainer.appendChild(nameContainer);
nameContainer.innerText = "Name: Batil";
exports.aaa = 5;

},{}],3:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const utils_1 = require("../utils/utils");
const handleButtonClick = () => {
    console.log("dismissing notification");
};
const createNoticifaction = (text, header) => {
    return utils_1.createDocumentFragmentFromString(`<div class="alert alert-dismissible show" role="alert">
  <div><strong>${header}</strong> ${text}</div>
  <div class="notification_button_wrapper"><button type="button" class="close" data-dismiss="alert" aria-label="Close"
  onclick="handleButtonClick()">
    <span aria-hidden="true">&times;</span>
  </button>
  </div>
</div>`);
};
exports.displayNotification = (text, header) => {
    const notificationsWrapper = document.getElementById("notifications");
    notificationsWrapper.appendChild(createNoticifaction(text, header));
};
exports.displayNotification("This is a notification. Click x to close it", "Notification 1.");
exports.displayNotification("This is a notification. Click x to close it. This is a long notification. This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification.", "Notification 2.");

},{"../utils/utils":5}],4:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const utils_1 = require("../utils/utils");
class ProgressBar {
    constructor(id, iconClassname, color, value, min, max) {
        this.id = `${id}-progress-bar`;
        this.iconClassname = iconClassname;
        this.value = value;
        this.min = min;
        this.max = max;
        this.color = color;
    }
    display() {
        const container = document.getElementById("progress_bars");
        const progressBarWrapper = document.createElement("div");
        progressBarWrapper.classList.add("progress_bar_wrapper");
        progressBarWrapper.id = `${this.id}-wrapper`;
        const infoContainer = document.createElement("div");
        infoContainer.classList.add("info");
        infoContainer.appendChild(utils_1.createDocumentFragmentFromString(`<i class="${this.iconClassname}"></i>`));
        progressBarWrapper.appendChild(infoContainer);
        this.valueContainer = document.createElement("div");
        this.valueContainer.innerText = `${this.value}/${this.max}`;
        this.valueContainer.classList.add("value");
        infoContainer.appendChild(this.valueContainer);
        this.progressBar = utils_1.createDocumentFragmentFromString(getProgressBarTemplate(this.id, this.value, this.min, this.max));
        progressBarWrapper.appendChild(this.progressBar);
        container.appendChild(progressBarWrapper);
        this.setValue(this.value);
        this.setBarColor(this.color);
        return this;
    }
    setBarColor(color) {
        if (color !== undefined) {
            this.color = color;
        }
        document.getElementById(this.id).style.backgroundColor = color;
        return this;
    }
    setValue(value) {
        if (value !== undefined) {
            this.value = value;
        }
        document.getElementById(this.id).style.width = `${Math.min(100, 100 * this.value / (this.max - this.min))}%`;
        return this;
    }
    setMin(min) {
        if (min !== undefined) {
            this.min = min;
        }
        return this;
    }
    setMax(max) {
        if (max !== undefined) {
            this.max = max;
        }
        return this;
    }
    update(value, min, max, color) {
        this.setMin(min);
        this.setMax(max);
        this.setValue(value);
        this.progressBar = utils_1.createDocumentFragmentFromString(getProgressBarTemplate(this.id, this.value, this.min, this.max));
        this.valueContainer.innerHTML = `${this.value}/${this.max}`;
        this.setBarColor(color);
        return this;
    }
}
exports.ProgressBar = ProgressBar;
function getProgressBarTemplate(id, value, min, max) {
    return `<div class="progress">
  <div id=${id} class="progress-bar" role="progressbar" aria-valuenow=${value} aria-valuemin=${min} aria-valuemax=${max}></div>
</div>`;
}
const progressBar1 = new ProgressBar("food", "icon fas fa-utensils", "yellow", 50, 0, 100).display();
const progressBar2 = new ProgressBar("carriage", "icon fas fa-weight-hanging", "yellow", 70, 0, 100).display();
setTimeout(() => {
    progressBar2.update(60, 0, 200, "pink");
}, 3000);
const progressBar3 = new ProgressBar("effort", "icon fas fa-hammer", "yellow", 88.88, 0, 88).display();

},{"../utils/utils":5}],5:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function createDocumentFragmentFromString(markup) {
    const template = document.createElement("template");
    template.innerHTML = markup.trim();
    console.log("CREATING MARKUP");
    return template.content;
}
exports.createDocumentFragmentFromString = createDocumentFragmentFromString;
;
console.log("UTILS");

},{}]},{},[1]);
