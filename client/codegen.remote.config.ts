import type { CodegenConfig } from '@graphql-codegen/cli';
import { config as envConfig } from 'dotenv';
import { configBase } from './codege.base.config';

envConfig({ path: '.env.development' });

const config: CodegenConfig = {
  ...configBase,
  schema: `${process.env.SERVER_UNSECURED_URL}/graphql?sdl`,
  generates: {
    ...configBase.generates,
    'src/gql/schema.json': {
      plugins: ['introspection'],
    },
  },
};

export default config;
