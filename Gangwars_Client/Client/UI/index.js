
//----------------------------------//
///// VenoX Gaming & Fun 2019 © ///////
//////By Solid_Snake & VnX RL Crew////
////////www.venox-reallife.com////////
//----------------------------------//
import * as alt from 'alt-client';
import * as game from "natives";
import { ShowCursor } from '../Lib';

let UI = new alt.WebView("http://resource/Client/UI/main.html");

alt.onServer('TeamSelection:Show', () => {
    UI.focus();
    ShowCursor(true);
    UI.emit('TeamSelection:Show');
    UI.on('selectedTeam', (team) => {
        UI.emit('TeamSelection:Hide');
        alt.emitServer('Gangwars:SelectTeam', team);
        ShowCursor(false);
    });
});

alt.onServer('TeamSelection:Hide', () => {

});
