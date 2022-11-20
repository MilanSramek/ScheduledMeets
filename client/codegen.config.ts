import type { CodegenConfig } from '@graphql-codegen/cli';
import { config as envConfig } from 'dotenv';

envConfig({ path: '.env.development' });

const config: CodegenConfig = {
  overwrite: true,
  schema: `${process.env.SERVER_UNSECURED_URL}/graphql?sdl`,
  documents: 'src/**/*.tsx',
  generates: {
    'src/gql/': {
      preset: 'client',
      plugins: [],
    },
    'src/gql/schema.json': {
      plugins: ['introspection'],
    },
  },
};

export default config;
