/* tslint:disable */
/* eslint-disable */
import { Comment } from './comment';
import { Img } from './img';
import { User } from './user';
export interface CaffEntity {
  address?: null | string;
  comments?: null | Array<Comment>;
  creator?: null | string;
  id?: number;
  images?: null | Array<Img>;
  owner?: User;
  ownerId?: string;
}
