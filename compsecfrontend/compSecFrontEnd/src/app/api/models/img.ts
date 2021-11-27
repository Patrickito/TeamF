/* tslint:disable */
/* eslint-disable */
import { CaffEntity } from './caff-entity';
import { Tag } from './tag';
export interface Img {
  address?: null | string;
  caff?: CaffEntity;
  caffId?: number;
  caption?: null | string;
  id?: number;
  tags?: null | Array<Tag>;
}
