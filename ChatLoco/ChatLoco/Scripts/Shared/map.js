﻿
function MapObject() {

    var _initMap = null;

    var initMap = function () {

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            alert("Geolocation is not supported by this browser.");
        }

        function showPosition(position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude
            $("#lat").html(lat);
            $("#lon").html(lon);

            getNearbyPlaces(lat, lon);
        }

        function getNearbyPlaces(lat, lon) {
            var loc = { lat: lat, lng: lon };

            map = new google.maps.Map(document.getElementById('map'), {
                center: loc,
                zoom: 19
            });

            $(window).resize(function () {
                // (the 'map' here is the result of the created 'var map = ...' above)
                google.maps.event.trigger(map, "resize");
            });

            $radius = $("#slider").slider("value");
            console.log("Radius changed: " + $radius)

            var request = {
                location: loc,
                radius: $radius,
            };

            service = new google.maps.places.PlacesService(map);
            service.nearbySearch(request, callback);

            function callback(results, status) {

                if (status == google.maps.places.PlacesServiceStatus.OK) {

                    var bounds = new google.maps.LatLngBounds();

                    $("#chatroomPlaces").html("");
                    for (i = 0; i < results.length; i++) {
                        marker = new google.maps.Marker({
                            position: new google.maps.LatLng(results[i].geometry.location.lat(), results[i].geometry.location.lng()),
                            map: map,
                            animation: google.maps.Animation.DROP,
                            title: 'Chatroom: ' + results[i].name
                        });

                        bounds.extend(marker.getPosition());
                     
                        jQuery('#chatroomPlaces').append(jQuery("<option></option>").val(results[i]['place_id']).text(results[i]['name']));
                    }

                    map.fitBounds(bounds);
                }
            }

        }

        return {
            getNearbyPlaces: getNearbyPlaces
        }
    }

    var getInitMap = function () {
        return _initMap;
    }

    var init = function () {
        _initMap = initMap();

        initMap();
    }

    return {
        init: init,
        getInitMap: getInitMap
    }
}

var MapHandler = new MapObject();


