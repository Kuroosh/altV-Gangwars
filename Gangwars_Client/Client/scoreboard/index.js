
//----------------------------------//
///// VenoX Gaming & Fun 2019 © ///////
//////By Solid_Snake & VnX RL Crew////
////////www.venox-reallife.com////////
//----------------------------------//
import * as alt from 'alt-client';
import * as game from "natives";
import { GetCursorStatus, ShowCursor } from '../Lib';

let Scoreboard

export function CreateScoreboard() {
    if (Scoreboard) { return; }
    Scoreboard = new alt.WebView("http://resource/Client/scoreboard/main.html");
}

alt.on('keyup', (key) => {
    try {
        if (key == 0x59) {
            if (!Scoreboard) { return; }
            Scoreboard.emit("Scoreboard:Hide");
            game.displayHud(true);
            ShowCursor(false);
        }
    }
    catch{ }
});

alt.on('keydown', (key) => {
    try {
        if (key == 0x59) {
            if (!GetCursorStatus()) {
                if (!Scoreboard) { return; }
                game.displayHud(false);
                Scoreboard.focus();
                ShowCursor(true);
                Scoreboard.emit("Scoreboard:Show");
            }
        }
    }
    catch{ }
});


alt.onServer('Scoreboard:Update', (pl_li) => {
    if (Scoreboard != null) {
        Scoreboard.emit("FillScoreboard", pl_li);
        //alt.log(Object.keys(pl_li));
    }
});