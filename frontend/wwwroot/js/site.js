/*Configures the inital map state and options*/
function mapSetup() {
    //Initial map + home marker position
    const initX = 35.15608617490063;
    const initY = -90.05190639489136;

    //So user can't wrap around world + see gray zones at poles
    const bounds = [
        [-88, -180],
        [88, 180],
    ];
    const map = L.map('map', {
        maxZoom : 17, 
        maxBounds: bounds,
        maxBoundsViscosity: 1.0,
        zoomControl : false}).setView([initX, initY], 13);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
                minZoom: 3,
                attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
             }).addTo(map);

    //NOTE -- will have to put this somewhere accessible (to get things like location )
    const homemarker = L.marker([initX, initY], {draggable: true, autoPan: true}).addTo(map);
    
}



mapSetup();

// const map = L.map('map', {zoomControl : false, worldCopyJump : true}).setView([35.15608617490063, -90.05190639489136], 13);

// L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
//     maxZoom: 19,
//     attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
// }).addTo(map);

// var marker = L.marker([35.15608617490063, -90.05190639489136], {draggable: true, autoPan: true}).addTo(map);



