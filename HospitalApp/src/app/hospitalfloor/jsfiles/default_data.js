import * as THREE from "three";
import Orientation from "./orientation.js";

export const generalData = {
    setDevicePixelRatio: false
}

export const hospitalData = {
    url: "./config.json",
    scale: new THREE.Vector3(1.0, 1.0, 1.0)
}

export const lightsData = {
    ambientLight: { color: 0xffffff, intensity: 0.5 },
    directionalLight: { color: 0xffffff, intensity: 7.0, target: new THREE.Vector3(0.0, 0.0, 0.0), position: new THREE.Vector3(5,3,5) } // angle and direction expressed in radians
}

export const cameraData = {
    view: "fixed", // Fixed view: "fixed"; first-person view: "first-person"; third-person view: "third-person"; top view: "top"; mini-map: "mini-map"
    multipleViewsViewport: new THREE.Vector4(0.0, 0.0, 1.0, 1.0), // Viewport position and size: fraction of window width and window height; MUST BE REDEFINED when creating an instance of ThumbRaiser() so that each view is assigned a different viewport
    target: new THREE.Vector3(0.0, 0.0, 0.0), // Target position
    initialOrientation: new Orientation(135.0, -45.0), // Horizontal and vertical orientation and associated limits (expressed in degrees)
    orientationMin: new Orientation(-180.0, -90.0),
    orientationMax: new Orientation(180.0, 0.0),
    initialDistance: 8.0, // Distance to the target and associated limits
    distanceMin: 4.0,
    distanceMax: 16.0,
    initialZoom: 1.0, // Zoom factor and associated limits
    zoomMin: 0.5,
    zoomMax: 2.0,
    initialFov: 45.0, // Field-of-view (expressed in degrees)
    near: 0.01, // Front clipping plane
    far: 100.0 // Back clipping plane
}