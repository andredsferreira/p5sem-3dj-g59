import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import configJson from '../../../urls.json'
import { BACKEND_API_PATH, EXPRESS_BACKEND_API_PATH, IAM_API_PATH, PLANNING_API_PATH } from './config-path';

export const backend_path = configJson.backend;
export const express_backend_path = configJson.expressbackend;
export const planning_path = configJson.planning;
export const iam_path = configJson.iam;

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    {provide: BACKEND_API_PATH, useValue: backend_path},
    {provide: EXPRESS_BACKEND_API_PATH, useValue: express_backend_path},
    {provide: PLANNING_API_PATH, useValue: planning_path},
    {provide: IAM_API_PATH, useValue: iam_path}
  ]
};
