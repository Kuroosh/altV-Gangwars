
//----------------------------------//
///// VenoX Gaming & Fun 2019 © ///////
//////By Solid_Snake & VnX RL Crew////
////////www.venox-reallife.com////////
//----------------------------------//
import * as alt from 'alt-client';
import * as game from "natives";
import { ShowCursor } from '../Lib';
import { CreateScoreboard } from '../scoreboard';

let LoginRegisterBrowser;

alt.onServer('LoginRegister:Create', () => {
    LoginRegisterBrowser = new alt.WebView("http://resource/Client/RegisterLogin/main.html");
    LoginRegisterBrowser.focus();
    ShowCursor(true);
    LoginRegisterBrowser.on('Window:LoginClicked', (name, password) => {
        alt.emitServer('Gangwars:Login', name, password);
    });
    LoginRegisterBrowser.on('Window:RegisterClicked', (name, password) => {
        alt.emitServer('Gangwars:Register', name, password);
    });
});

alt.onServer('LoginRegister:Destroy', () => {
    if (LoginRegisterBrowser) {
        LoginRegisterBrowser.destroy();
        LoginRegisterBrowser = null;
        ShowCursor(false);
        CreateScoreboard();
    }
});