//----------------------------------//
///// VenoX Gaming & Fun 2019 © ///////
//////By Solid_Snake & VnX RL Crew////
////////www.venox-reallife.com////////
//----------------------------------//


if ("alt" in window) {
    alt.on('CarWindow:Show', (level) => {
        $('.team').removeClass('d-none');
        for (let i = 0; i < level; i++) {
            $('.c_' + i).removeClass('d-none');
            $('.c_' + level).removeClass('d-none');
        }
    });
    alt.on('CarWindow:Hide', () => {
        $('.team').addClass('d-none');
    });
}