import expressLoader from './express';
import dependencyInjectorLoader from './dependencyInjector';
import mongooseLoader from './mongoose';
import Logger from './logger';

import config from '../../config';

export default async ({ expressApp }) => {
  const mongoConnection = await mongooseLoader();
  Logger.info('✌️ DB loaded and connected!');

  const userSchema = {
    // compare with the approach followed in repos and services
    name: 'userSchema',
    schema: '../persistence/schemas/userSchema',
  };

  const roleSchema = {
    // compare with the approach followed in repos and services
    name: 'roleSchema',
    schema: '../persistence/schemas/roleSchema',
  };

  const medConditionSchema = {
    // compare with the approach followed in repos and services
    name: 'medConditionSchema',
    schema: '../persistence/schemas/medConditionSchema',
  };

  const allergySchema = {
    name: "allergySchema",
    schema: "../persistence/schemas/allergySchema"
  }

  const roleController = {
    name: config.controllers.role.name,
    path: config.controllers.role.path
  }

  const roleRepo = {
    name: config.repos.role.name,
    path: config.repos.role.path
  }

  const roleService = {
    name: config.services.role.name,
    path: config.services.role.path
  }

  const medConditionController = {
    name: config.controllers.medCondition.name,
    path: config.controllers.medCondition.path
  }

  const medConditionRepo = {
    name: config.repos.medCondition.name,
    path: config.repos.medCondition.path
  }

  const medConditionService = {
    name: config.services.medCondition.name,
    path: config.services.medCondition.path
  }

  const userRepo = {
    name: config.repos.user.name,
    path: config.repos.user.path
  }

  const allergyController = {
    name: config.controllers.allergy.name,
    path: config.controllers.allergy.path
  }

  const allergyRepo = {
    name: config.repos.allergy.name,
    path: config.repos.allergy.path
  }

  const allergyService = {
    name: config.services.allergy.name,
    path: config.services.allergy.path
  }

  await dependencyInjectorLoader({
    mongoConnection,
    schemas: [
      userSchema,
      roleSchema,
      medConditionSchema,
      allergySchema
    ],
    controllers: [
      roleController,
      medConditionController,
      allergyController,
    ],
    repos: [
      roleRepo,
      userRepo,
      medConditionRepo,
      allergyRepo
    ],
    services: [
      roleService,
      medConditionService,
      allergyService
    ]
  });
  Logger.info('✌️ Schemas, Controllers, Repositories, Services, etc. loaded');

  await expressLoader({ app: expressApp });
  Logger.info('✌️ Express loaded');
};
