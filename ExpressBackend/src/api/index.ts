import { Router } from 'express';
import auth from './routes/userRoute';
import user from './routes/userRoute';
import role from './routes/roleRoute';
import medCondition from './routes/medConditionRoute';
import allergy from "./routes/allergyRoute";
import medicalRecord from './routes/medicalRecordRoute';
import familyHistoryEntry from './routes/familyHistoryEntryRoute';

export default () => {
	const app = Router();

	auth(app);
	user(app);
	role(app);
	medCondition(app);
	allergy(app);
	medicalRecord(app);
	familyHistoryEntry(app);
	
	
	return app
}