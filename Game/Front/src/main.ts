console.log("main");
fetch("api/characters",
    {
        mode: "no-cors"
    }).then(a => {
        console.log(a.text())
    });

// declare module "./main/main" {
//     interface TreeContext {
//         serieVisibility(): SerieVisibility;
//     }
// }