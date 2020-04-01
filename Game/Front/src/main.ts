console.log("main");
fetch("https://localhost:5001/api/characters").then(a => {
    console.log(a)
});
