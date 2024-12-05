import { Router } from 'express';
import auth from './routes/userRoute';
import user from './routes/userRoute';
import role from './routes/roleRoute';
import medCondition from './routes/medConditionRoute';

export default () => {
	const app = Router();

	auth(app);
	user(app);
	role(app);
	medCondition(app);
	
	return app
}