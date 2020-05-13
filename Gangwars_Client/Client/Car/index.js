//----------------------------------//
///// VenoX Gaming & Fun 2019 © ///////
//////By Solid_Snake & VnX RL Crew////
////////www.venox-reallife.com////////
//----------------------------------//

import alt from 'alt-client';
import * as game from "natives";
import { ShowCursor, Draw3DText } from '../Lib';



let TextLabels = {};
let TextLabelsCounter = 0;
export function AddTextlabels(msg, x, y, z, font, r, g, b, range) {
    TextLabels[++TextLabelsCounter] = {
        msg: msg,
        x: x,
        y: y,
        z: z,
        font: font,
        r: r,
        g: g,
        b: b,
        range: range
    };
}
alt.everyTick(() => {
    for (var clabel in TextLabels) {
        let labels = TextLabels[clabel];
        Draw3DText(labels.msg, labels.x, labels.y, labels.z, labels.font, [labels.r, labels.g, labels.b, 255], labels.range);
    }
    game.setEntityProofs(alt.Player.local.scriptID, true, false, false, true, false);
});



let CarWindow = new alt.WebView("http://resource/Client/Car/main.html");

alt.onServer('CarWindow:Show', (level) => {
    CarWindow.focus();
    ShowCursor(true);
    CarWindow.emit("CarWindow:Show", level);
    CarWindow.on('car:select', (numb) => {
        alt.emitServer('Car:SpawnVehicle', numb);
    })
    CarWindow.on('CarWindow:Hide', () => {
        CarWindow.emit('CarWindow:Hide');
        ShowCursor(false);
    });
});

alt.onServer('CarWindow:Hide', () => {
    CarWindow.emit('CarWindow:Hide');
    ShowCursor(false);
});



alt.on('keydown', (key) => {
    try {
        if (key == 0x45) {
            alt.emitServer("OnPressedEventKey");
        }
    }
    catch{ }
});