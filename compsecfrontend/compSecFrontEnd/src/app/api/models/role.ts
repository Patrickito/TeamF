/* tslint:disable */
/* eslint-disable */
import { User } from './user';
export interface Role {
  id?: number;
  name: string;
  users?: null | Array<User>;
}
