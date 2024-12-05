import { IMedConditionPersistence } from '../../dataschema/IMedConditionPersistence';
import mongoose from 'mongoose';

const MedConditionSchema = new mongoose.Schema(
  {
    domainId: { type: String, unique: true },
    code: { type: String, unique: true},
    designation: { type: String, unique: true },
    description: { type: String, unique: false, required: false }
  },
  {
    timestamps: true
  }
);

export default mongoose.model<IMedConditionPersistence & mongoose.Document>('MedicalCondition', MedConditionSchema);
