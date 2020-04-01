console.log("main");
fetch("api/characters").then(a => {
    console.log(a.text())
});
