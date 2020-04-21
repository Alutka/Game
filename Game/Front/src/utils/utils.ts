export function createDocumentFragmentFromString(markup: string): DocumentFragment {
    const template = document.createElement("template");
    template.innerHTML = markup.trim();
    console.log("CREATING MARKUP");
    return template.content;
};
 console.log("UTILS");