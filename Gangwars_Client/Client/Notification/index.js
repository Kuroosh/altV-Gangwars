//----------------------------------///
///// VenoX Gaming & Fun 2019 © ///////
///////////////////////////////////////
////////www.venox-reallife.com/////////
//----------------------------------///

import * as alt from 'alt-client';
import * as game from "natives";

let browser_1 = new alt.WebView("http://resource/Client/Notification/notify.html", false);

alt.onServer('createVnXLiteNotify', (e, v) => {
    browser_1.emit("Notify:Create", e, v);
});

alt.onServer('Globals:PlayHitsound', () => {
    browser_1.emit('Notify:PlayHitSound');
    VnXTM = Date.now() / 1000;
});

alt.onServer('Globals:ShowBloodScreen', () => {
    browser_1.emit('Notify:BloodScreen');
});


var VnXTM = 0;
alt.everyTick(() => {
    game.resetPlayerStamina(alt.Player.local.scriptID);
    if (!game.hasStreamedTextureDictLoaded("hud_reticle")) {
        game.requestStreamedTextureDict("hud_reticle", true);
    }
    if (game.hasStreamedTextureDictLoaded("hud_reticle")) {
        if ((Date.now() / 1000 - VnXTM) <= 0.1) {
            game.drawSprite("hud_reticle", "reticle_ar", 0.5, 0.5, 0.025, 0.040, 45, 255, 255, 255, 150);
        }
    }
});