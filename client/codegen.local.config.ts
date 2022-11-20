import type { CodegenConfig } from '@graphql-codegen/cli';
import { configBase } from './codege.base.config';

const config: CodegenConfig = {
  ...configBase,
  schema: 'src/gql/schema.json',
};

export default config;
