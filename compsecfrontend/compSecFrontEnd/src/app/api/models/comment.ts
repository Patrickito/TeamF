/* tslint:disable */
/* eslint-disable */
import { CaffEntity } from './caff-entity';
import { User } from './user';
export interface Comment {
  caffEntity?: CaffEntity;
  caffEntityId?: number;
  commentText?: null | string;
  dateTime?: string;
  id?: number;
  user?: User;
  userId?: string;
}
