import * as THREE from "three";
import Wall from "./wall";
import Ground from "./ground";
import * as THREE from "three";
import { Room } from "../room";

interface LoaderParameters {
    room: Room;
    leftSide: boolean;
    map: number[][];
    roomSize: Vector2;
    wallTextureUrl: string;
    wallSize: Vector3;
    table: ObjMtl;
    tableWithPerson: ObjMtl;
    door: Fbx;
    bench: Fbx;
    cart: Fbx;
    oxygene: Fbx;
    doctor: Fbx;
}

interface ObjMtl {
    url: string,
    obj: string,
    mtl: string
}

interface Fbx {
    url: string,
    fbx: string
}

interface Vector2 {
    width: number;
    height: number;
}

interface Vector3 {
    width: number;
    height: number;
    depth: number;
}

export default class RoomLoader {
    room: Room;
    leftSide: boolean;
    scale: THREE.Vector3;
    map?: number[][];
    size?: MazeSize;
    object: THREE.Group;
    loaded: boolean;
    table: THREE.Object3D;

    constructor(parameters: LoaderParameters);
    
    toggleTableVisibility(occupied: boolean|null);

    isTable(object: THREE.Object3D): boolean;  
}
