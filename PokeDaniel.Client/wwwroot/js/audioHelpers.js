var poke = new Audio();
poke.src = "/audio/poke.mp3";

var duck = new Audio();
duck.src = "/audio/duck.mp3";

var mandel = new Audio();
mandel.src = "/audio/mandel.mp3";

window.playPoke = function () {
    poke.play();
}

window.playDuck = function () {
    duck.play();
}

window.playMandel = function () {
    mandel.play();
}
