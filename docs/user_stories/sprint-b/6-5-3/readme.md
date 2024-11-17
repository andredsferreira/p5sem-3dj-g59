# US 6.5.3 - Light the Hospital Floor

As a **healthcare staff member**, I want to see the hospital/clinic floor illuminated with ambient and directional light.

## 1. Context

This **US** is part of the **3D visualization module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The team must apply an ambient light and a directional light to the hospital floor.
2. These lights must create shadows.

### 2.2. Dependencies

* No dependencies (explained in **2.3. Pre-Conditions**).

### 2.3. Pre-Conditions

* Although the lights are cast onto the **Hospital floor** created in [**US 6-5-1**](../6-5-1/readme.md), the lights can be easily created on a template object.

### 2.4. Open Questions

* No Open Questions

## 3. Analysis

Not applicable here, as this functionality has no business logic.

## 4. Design

Not applicable here, as this functionality doesn't leave room for design decisions.

## 5. C4 Views

-

## 6. Tests

* Test if the lights are being cast correctly.
* Test if shadows are being created.

Both of these tests are represented in these methods:

```ts
  it('should add ambient light to the scene', () => {
    const light = new THREE.AmbientLight(0xffffff, 0.5);
    scene.add(light);

    const ambientLight = scene.children.find(
      (child) => child instanceof THREE.AmbientLight
    ) as THREE.AmbientLight;

    expect(ambientLight).toBeTruthy();
    expect(ambientLight.color.getHex()).toBe(0xffffff);
    expect(ambientLight.intensity).toBe(0.5);
  });
```

```ts
  it('should add directional light with shadows enabled to the scene', () => {
    const directionalLight = new THREE.DirectionalLight(0xffffff, 3);
    directionalLight.castShadow = true;
    directionalLight.position.set(5, 3, 5);
    directionalLight.lookAt(new THREE.Vector3(0, 0, 0));

    directionalLight.shadow.mapSize.width = 2048;
    directionalLight.shadow.mapSize.height = 2048;

    directionalLight.shadow.camera.left = -15;
    directionalLight.shadow.camera.right = 15;
    directionalLight.shadow.camera.top = 5;
    directionalLight.shadow.camera.bottom = -10;
    directionalLight.shadow.camera.near = -15;
    directionalLight.shadow.camera.far = 25;

    scene.add(directionalLight);

    const addedDirectionalLight = scene.children.find(
      (child) => child instanceof THREE.DirectionalLight
    ) as THREE.DirectionalLight;

    expect(addedDirectionalLight).toBeTruthy();
    expect(addedDirectionalLight.color.getHex()).toBe(0xffffff);
    expect(addedDirectionalLight.intensity).toBe(3);

    // Verify shadow settings
    expect(addedDirectionalLight.castShadow).toBeTrue();
    expect(addedDirectionalLight.shadow.mapSize.width).toBe(2048);
    expect(addedDirectionalLight.shadow.mapSize.height).toBe(2048);

    // Verify shadow camera bounds
    const shadowCamera = addedDirectionalLight.shadow.camera as THREE.OrthographicCamera;
    expect(shadowCamera.left).toBe(-15);
    expect(shadowCamera.right).toBe(15);
    expect(shadowCamera.top).toBe(5);
    expect(shadowCamera.bottom).toBe(-10);
    expect(shadowCamera.near).toBe(-15);
    expect(shadowCamera.far).toBe(25);
  });
```

## 7. Implementation

**hospitalfloor.component.ts**

```ts
const light = new THREE.AmbientLight(0xffffff, 0.5);
this.scene.add(light);

const directionalLight = new THREE.DirectionalLight( 0xffffff,3);
directionalLight.castShadow = true;
directionalLight.position.set( 5,3,5 );
directionalLight.lookAt(new THREE.Vector3(0,0,0));

directionalLight.shadow.mapSize.width = 2048;
directionalLight.shadow.mapSize.height = 2048;

directionalLight.shadow.camera.left = -15;
directionalLight.shadow.camera.right = 15;
directionalLight.shadow.camera.top = 5;
directionalLight.shadow.camera.bottom = -10;
directionalLight.shadow.camera.near = -15;
directionalLight.shadow.camera.far = 25;

this.scene.add(directionalLight);
```

The ambient light lights the entire scene, so it doesn't cast a shadow. 

The directional light is stronger and applies from a certain angle (from **(5,3,5)** to **(0,0,0)**) and it applies shadows to the scene. It has a large enough **mapSize** to not have the shadows be too pixelated. Its camera must contain the entire scene.

Both lights are white, in order not to mess with the other objects' colors.

**Ground**

```js
this.object.castShadow = false;
this.object.receiveShadow = true;
```

The ground and floor must receive shadows from other objects, but not cast any shadows.

**Complex Objects (imported .obj)**

```js
obj.traverse((node) => {
    if (node instanceof THREE.Mesh) {
        node.receiveShadow = true;
        node.castShadow = true;
    }
});
```

In complex objects, we must make every one of its nodes receive and cast shadows.

**Other Objects**

```js
this.object.castShadow = true;
this.object.receiveShadow = true;
```

In other simple objects, it's only needed to add these 2 lines.

## 8. Demonstration

-