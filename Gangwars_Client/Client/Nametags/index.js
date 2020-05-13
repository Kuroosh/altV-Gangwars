//----------------------------------//
///// VenoX Gaming & Fun 2019 © ///////
//////By Solid_Snake & VnX RL Crew////
////////www.venox-reallife.com////////
//----------------------------------//

import alt from 'alt-client';
import * as game from "natives";
import { DrawText } from '../Lib';

let maxDistance = 20;
let maxDistance_load = 20;
let name = "";
alt.everyTick(() => {
    let players = alt.Player.all;
    if (players.length > 1) {
        let localPlayer = alt.Player.local;
        for (var i = 0; i < players.length; i++) {
            var player = players[i];
            if (player.getStreamSyncedMeta("PLAYER_NAME") == localPlayer.getStreamSyncedMeta("PLAYER_NAME")) { return; }
            name = player.getStreamSyncedMeta("PLAYER_NAME");
            let playerPos = localPlayer.pos;
            let playerPos2 = player.pos;
            let distance = game.getDistanceBetweenCoords(playerPos.x, playerPos.y, playerPos.z, playerPos2.x, playerPos2.y, playerPos2.z, true);
            if (player.vehicle && localPlayer.vehicle) { maxDistance_load = 60; } else { maxDistance_load = maxDistance; }
            let screenPos = game.getScreenCoordFromWorldCoord(playerPos2.x, playerPos2.y, playerPos2.z + 1);
            if (distance <= maxDistance_load) {
                DrawText(name, [screenPos[1], screenPos[2] - 0.030], [0.65, 0.65], 4, [255, 255, 255, 255], true, true);
            }
        }
    }
});