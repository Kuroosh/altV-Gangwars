
//----------------------------------//
///// VenoX Gaming & Fun 2019 © ///////
//////By Solid_Snake & VnX RL Crew////
////////www.venox-reallife.com////////
//----------------------------------//
import * as alt from 'alt-client';
import * as game from "natives";
import { AddTextlabels } from '../Car';

let LocalPlayer = alt.Player.local;

alt.onServer('Player:Freeze', (bool) => {
    try {
        game.freezeEntityPosition(LocalPlayer.scriptID, bool);
    }
    catch{ }
});

alt.onServer('Player:Spawn', () => {
    try {
        game.displayHud(true);
    }
    catch{ }
});

alt.onServer('Vehicle:Freeze', (veh, bool) => {
    try {
        game.freezeEntityPosition(veh.scriptID, bool);
    }
    catch{ }
});
alt.onServer('Vehicle:Godmode', (veh, bool) => {
    try {
        game.setEntityInvincible(veh.scriptID, bool);
    }
    catch{ }
});
alt.onServer('Vehicle:Repair', (veh) => {
    try {
        game.setVehicleFixed(veh.scriptID);
    }
    catch{ }
});


alt.onServer("BlipClass:CreateBlip", (BlipJson) => {
    try {
        let Blip = JSON.parse(BlipJson);
        for (let i = 0; i < Blip.length; i++) {
            let data_blip = Blip[i];
            //alt.log("Datas : " + data_blip.Name + " | " + [data_blip.posX, data_blip.posY, data_blip.posZ] + " | " + data_blip.Sprite + " | " + data_blip.Color + " | " + data_blip.ShortRange);
            CreateBlip(data_blip.Name, [data_blip.posX, data_blip.posY, data_blip.posZ], data_blip.Sprite, data_blip.Color, data_blip.ShortRange);
        }
    }
    catch{ }
});

alt.onServer("Clothes:Load", (clothesslot, clothesdrawable, clothestexture, clothespalette) => {
    try {
        if (clothesdrawable < 0 || clothestexture < 0) { return; }
        game.setPedComponentVariation(LocalPlayer.scriptID, clothesslot, clothesdrawable, clothestexture, clothespalette);
    }
    catch{ }
});

alt.onServer("Hair:Color", (color1, color2) => {
    try {
        game.setPedHairColor(LocalPlayer.scriptID, color1, color2);
    }
    catch{ }
});

alt.onServer("Prop:Load", (clothesslot, clothesdrawable, clothestexture) => {
    try {
        if (clothesdrawable < 0 || clothestexture < 0) { return; }
        game.setPedPropIndex(LocalPlayer.scriptID, clothesslot, clothesdrawable, clothestexture, true);
    }
    catch{ }
});



alt.onServer("TextLabel:Create", (msg, x, y, z, font, r, g, b, range) => {
    AddTextlabels(msg, x, y, z, font, r, g, b, range);
});

alt.onServer("Accessories:Load", (clothesslot, clothesdrawable, clothestexture) => {
    try {
        game.setPedPreloadVariationData(LocalPlayer.scriptID, clothesslot, clothesdrawable, clothestexture);
    }
    catch{ }
});


alt.onServer('Player:Visible', (bool) => {
    try {
        game.setEntityVisible(LocalPlayer.scriptID, bool, 0);
    }
    catch{ }
});

alt.onServer('Player:Alpha', (alpha) => {
    try {
        game.setEntityAlpha(Entity.scriptID, alpha, true);
    }
    catch{ }
});

alt.onServer('Player:WarpIntoVehicle', (veh, seat) => {
    try {
        alt.setTimeout(() => {
            game.taskWarpPedIntoVehicle(LocalPlayer.scriptID, veh.scriptID, seat);
        }, 500);
    }
    catch{ }
});

alt.onServer('Player:WarpOutOfVehicle', () => {
    try {
        if (LocalPlayer.vehicle) {
            game.taskLeaveVehicle(alt.Player.local.scriptID, LocalPlayer.vehicle.scriptID, 16);
        }
    }
    catch{ }
});


let Greenzone = {};
alt.onServer('Zone:Create', (n, x, y, z, r, c, r2) => {
    try {
        if (Greenzone[n] != null) {
            game.removeBlip(Greenzone[n]);
        }
        Greenzone[n] = game.addBlipForRadius(x, y, z, r);

        game.setBlipSprite(Greenzone[n], 5);
        game.setBlipAlpha(Greenzone[n], 150);
        game.setBlipColour(Greenzone[n], c);
        game.setBlipRotation(Greenzone[n], r2);
    }
    catch{ }
});



//mp.events.add = Event was Client & serverside called werden kann.
// alt.onServer = Event was soweit ich weiß nur vom Server gecalled werden kann.
// Nutze für ClientEvents bitte alt.onServer