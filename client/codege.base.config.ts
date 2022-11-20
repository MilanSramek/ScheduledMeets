import { CodegenConfig } from '@graphql-codegen/cli';

export const configBase: CodegenConfig = {
  overwrite: true,
  documents: 'src/**/*.tsx',
  generates: {
    'src/gql/': {
      preset: 'client',
      plugins: [],
    },
  },
};
