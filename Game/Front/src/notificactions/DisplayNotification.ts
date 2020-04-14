const handleButtonClick = ()=>{
    console.log("dismissing notification");
};

const createNoticifaction = (text: string, header?: string)=>{
    return createDocumentFragmentFromString(`<div class="alert alert-dismissible show" role="alert">
  <div><strong>${header}</strong> ${text}</div>
  <div class="notification_button_wrapper"><button type="button" class="close" data-dismiss="alert" aria-label="Close">
    <span aria-hidden="true">&times;</span>
  </button>
  </div>
</div>`)
};

const displayNotification = (text: string, header?: string)=>{
    const notificationsWrapper = document.getElementById("notifications");
    notificationsWrapper.appendChild(createNoticifaction(text, header))
};

const createDocumentFragmentFromString = (markup: string): DocumentFragment => {
    const template = document.createElement("template");
    template.innerHTML = markup.trim();
    return template.content;
};


displayNotification("This is a notification. Click x to close it", "Notification 1.");
displayNotification("This is a notification. Click x to close it. This is a long notification. This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification." +
    "This is a long notification.This is a long notification.This is a long notification.", "Notification 2.");
