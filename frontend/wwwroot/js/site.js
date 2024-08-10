/*Global user data object*/ 
const user = userSetup();

/*Sets up the user object representing all the data*/
function userSetup() {
    const user = {
        lat: 35.156038923689195,
        lng: -90.05194020924256,
        //The following initialized by mapsetup fn
        locationMarker: undefined, //Draggable location pin
        activePlaceMarkers: [], //Leaflet marker objects for place rsults
        map: undefined //NOTE - might b a better way here
    };
    return user;
}

/*Configures the inital map state and options*/
function mapSetup() {
    //Initial map + home marker position
    const initX = user.lat;
    const initY = user.lng;

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

    //Set up the search location marker
    const homemarker = L.marker([initX, initY], {
        alt: 'search location',
        draggable: true, 
        autoPan: true}).addTo(map);

    homemarker.addEventListener('move', (e) => {
        user.lat = e.latlng.lat;
        user.lng = e.latlng.lng;
    });

    user.locationMarker = homemarker;
    return map;
}

/*Updates the search location to the user's current position*/
//TODO -- might wanna store user location in a cookie / localstorage 4 future visits
function getUserLocation() {
    if(!('geolocation' in navigator)){
        alert('Geolocation services are not available in your browser.');
        return;
    }
    
    //TODO -- might be nice to add some loading flavortext, as it looks like
    //the geolocation api could take a bit sometimes
    function success(pos) {
        const coords = pos.coords;
        user.lat = coords.latitude;
        user.lng = coords.longitude;
        
        //Move the home icon and pan the map round
        user.locationMarker.setLatLng([user.lat, user.lng]);
        user.map.panTo([user.lat, user.lng])
    }

    function fail(error) {
        console.warn(`ERROR(${error.code}): ${error.message}`);

        //TODO -- might be nice if this prompt was a styled css bubble instead of an alert
        if (error.code === 1) {
            alert('Geolocation denied by user. Please refresh the page to enable geolocation.');
            return;
        }

        alert('There was an error with the geolocation.');
    }

    navigator.geolocation.getCurrentPosition(success, fail);
}

//FIXME -- anim not playing on first click,
//These 2 fns oggle the slidein / slideuot animation
function slideinIfHidden(el) {
    //Display the sliding in animation
    if (el.style.display === 'none') {
        el.classList.add('slidein-left'); //only animate if hidden
    }
    el.style.display = 'block'; 
}

//Remove slidein classes @ the end of the animation
function removeSlideinClassesAtEnd(el) {
    el.addEventListener('animationend', () => {
        if (el.classList.contains('slideout-left')) {
            //Needs to occur on animaton end so it dissapears after it's out of sight
            el.style.display = 'none';
        }
    
        //Safe bc methods do nothing if class not present
        el.classList.remove('slidein-left');
        el.classList.remove('slideout-left');
    });
}

//From gpt lol
function stringToTime(timeOnly) {

    // Get the current date
    const currentDate = new Date();
    
    // Extract the parts of the timeOnly string
    const [hours, minutes, seconds] = timeOnly.split(':').map(Number);
    
    // Create a Date object with the current date and extracted time
    const combinedDate = new Date(
        currentDate.getFullYear(),
        currentDate.getMonth(),
        currentDate.getDate(),
        hours,
        minutes,
        seconds
    );
    
    return combinedDate;
}

function renderHoursInfo(openingHours) {
    if (openingHours === null) {
        return;
    }

    const hours = Object.values(openingHours).slice(1); //remove isOpen prop
    
    //get + clear the hourboxes before rendering
    const hourBoxes = document.querySelectorAll(
        '.info-hours-entry p:last-child'
    );
    Array.from(hourBoxes).map(e => e.innerText = '');

    //hours.length  === hourBoxes.length === 7
    for(let i = 0; i < 7; i++) {
        const timeRange = hours[i];
        const dayBox = hourBoxes[i];

        if (timeRange === null) {
            dayBox.innerText = 'Closed';
            continue;
        }
        //console.log(timeRange);
        const start = stringToTime(timeRange.start);
        const end = stringToTime(timeRange.end);
        
        if (start === end) {
            dayBox.innerText = 'Open 24 hours';
            continue;
        }

        //Make into HH:mm AM/PM strin
        
        //TODO -- for now this is en-US, in the future want to put to dynamically adapt to local fomat
        //ALso not as efficient apparently
        const options = {
            timeStyle : "short" //Display in hh:mm time format
        };
        const startStr = start.toLocaleTimeString("en-US", options); //not as eff see docs
        const endStr = end.toLocaleTimeString("en-US", options);
    
        dayBox.innerText = `${startStr} - ${endStr}`;

    }
}

function renderContactDropdown(contactType, contactArr) {
    //REQ: contactArr length >= 1

    //console.log(`${contactType}-dropdown`);
    const dropdown = document.getElementById(`${contactType}-dropdown`);
    const dropdownHeader = document.getElementById(`${contactType}-dropdown-head`);
    dropdown.innerHTML = ''; //Clear the previous elements NOTE may b slower than removing last elem
    for (let i = 0; i < dropdownHeader.children.length; i++) {
        const el = dropdownHeader.children[i];
        if (el !== dropdown) {
            dropdownHeader.removeChild(el);
        }
    }
    
    for (let i = 0; i < contactArr.length; i++) {
        const val = contactArr[i];
        const dropdownItem = document.createElement('div');
        dropdownItem.classList.add('contact-dropdown-item');

        if (contactType === 'web') {
            dropdownItem.innerHTML = `<a href="${val}" target="_blank"><p class="place-info-text">${val}</p></a>`;
        }
        else {
            dropdownItem.innerHTML = `<p class="place-info-text">${val}</p>`;
        }
       
        if (i === 0) {
            //First item should always show, and add to the header
            dropdownHeader.appendChild(dropdownItem);
        }
        else {
            dropdown.appendChild(dropdownItem);
        }
    }

    const toggleBtn = document.getElementById(`${contactType}-dropdown-btn`);
    if (contactArr.length === 1) {
        toggleBtn.style.visibility = 'hidden';
        
    }
    else {
        toggleBtn.style.visibility = 'visible';
    }
    //Show / hide the elems on button press
    const toggleDropdown = () => {
        const dropdownLocal = dropdown;
        dropdownLocal.classList.toggle('show');
    }

    toggleBtn.onclick = () => toggleDropdown(); //want to ovveride each time

}

//The dropdown menu rendered when the 'show more' btn of a contact type is pressed
function renderContactInfo(contacts) {
    if (contacts === null) {
        return;
    }

    const contactTypes = ['phone', 'web', 'email'];
    const contactBoxes = document.querySelectorAll('#place-info-contacts > div.place-info-box');

    for (let i = 0; i < 3; i++) {
        const contactType = contactTypes[i];
        const contactBox = contactBoxes[i];

        if (contacts[contactType].length === 0) {
            contactBox.style.display = 'none';
        }
        else {
            contactBox.style.display = 'flex'; //TODO check stylesheet
            //Display the contact info from the api in the dropdown
           renderContactDropdown(contactType, contacts[contactType]);
        }
    }
}

//Show images, contacts, etc when a map marker is pressed
function showPlaceInfo(place) {
    const titleBox = document.getElementById('place-title');
    const addressBox = document.getElementById('place-address');
    
    slideinIfHidden(document.getElementById('place-info'));
    //Render attributes 
    titleBox.textContent = place.title;
    addressBox.textContent = place.address;

    //If any section has NO data, dont render it
    const infoSections = document.getElementsByClassName('place-info-nullable');
    //need direct call here b/c getElements fn returns HTMLcollection, not array
    Array.prototype.forEach.call(infoSections, el => {
        const propertyName = el.dataset.name; 
        if (place[propertyName] === null) {
            el.style.display = 'none';
        }
        else {
            el.style.display = 'block'; //may not b block
        }
    });

    renderContactInfo(place.contacts);
    renderHoursInfo(place.openingHours);
}

function hideSidepanelInfo(el) {
    el.classList.add('slideout-left');  
    //Note the handlers at the end of the script
}

function clearExistingPlaceMarkers() {
    const markers = user.activePlaceMarkers;
    markers.forEach(marker => {
        user.map.removeLayer(marker);
    });
    user.activePlaceMarkers = [];
}

function setPlaceMarkers(places) {
    //TODO -- would b nice to make a custom popup displaying the title of the place w/ css
    //Kinda like how google maps does
    //Add markers in the results to the map
    //Could also add a small animation to the markers when they spawn in
    places.forEach(place => {
        const marker = L.marker([place.lat, place.lng], {
            title: place.title
        });
        
        marker.addEventListener('click', () => showPlaceInfo(place))
        
        user.activePlaceMarkers.push(marker);
        marker.bindTooltip(`${place.title}`); //Only works on hover but better than nothin
        marker.addTo(user.map);

    });
}

//Events for the filters n stuff

//TODO -- would be nice to inform the user if theres no search results near a given location (with a cool css popup, alert works tho)
async function search() {
    try { //TODO -- replace w/ actual backend location
        const response = await fetch("http://localhost:5057/search", {
            method: "POST",
            headers: {
                "Accept" : "application/json",
                "Content-Type" : "application/json"
            },
            body: JSON.stringify({
                lat: user.lat, 
                lng: user.lng
            })
        });

        const placeResults = await response.json();
        clearExistingPlaceMarkers();

        if (placeResults.length === 0) {
            alert("Sorry, couln't find any places. Adjust the search location or your filters and try again.");
            return;
        }

        setPlaceMarkers(placeResults);

    } catch (error) {
        console.warn(error.message);
        //Need to warn the client
    }
}

//Run setup functions + bind callbacks

//Initialise the map for the given session
user.map = mapSetup();

const filterBtn = document.getElementById('filter-btn');
const filterPanel = document.getElementById('filters');
const geolocateBtn = document.getElementById('geolocate');
const searchBtn = document.getElementById('search');

//This could b in a different spot
const placeInfo = document.getElementById('place-info');

//Bind event to remove slidein classes @ end of animation
removeSlideinClassesAtEnd(placeInfo);

//So user can't interact with map through the place info sidebar
L.DomEvent.disableClickPropagation(placeInfo);
L.DomEvent.disableScrollPropagation(placeInfo);

const closePlaceInfoBtn = document.getElementById('close-place-info');
closePlaceInfoBtn.addEventListener('click', () => hideSidepanelInfo(placeInfo));

geolocateBtn.addEventListener('click', () => getUserLocation());
searchBtn.addEventListener('click', () => search());

removeSlideinClassesAtEnd(filterPanel);
filterBtn.addEventListener('click', () => {
    slideinIfHidden(filterPanel);
} );
L.DomEvent.disableClickPropagation(filterPanel);
L.DomEvent.disableScrollPropagation(filterPanel);

const closeFilterBtn = document.getElementById('close-filters');
closeFilterBtn.addEventListener('click', () => hideSidepanelInfo(filterPanel));




