import * as THREE from "three";
import Orientation from "./orientation";

// Interface for generalData
export interface GeneralData {
    setDevicePixelRatio: boolean;
}

// Interface for hospitalData
export interface HospitalData {
    url: string;
    scale: THREE.Vector3;
}

// Interface for lightsData
export interface LightsData {
    ambientLight: {
        color: number;
        intensity: number;
    };
    directionalLight: {
        color: number;
        intensity: number;
        target: THREE.Vector3;
        position: THREE.Vector3;
    };
}

// Interface for cameraData
export interface CameraData {
    view: "fixed" | "first-person" | "third-person" | "top" | "mini-map";
    multipleViewsViewport: THREE.Vector4;
    target: THREE.Vector3;
    initialOrientation: Orientation;
    orientationMin: Orientation;
    orientationMax: Orientation;
    initialDistance: number;
    distanceMin: number;
    distanceMax: number;
    initialZoom: number;
    zoomMin: number;
    zoomMax: number;
    initialFov: number;
    near: number;
    far: number;
}
