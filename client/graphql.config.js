require('dotenv').config({ path: '.env.development' });

module.exports = {
  client: {
    service: {
      name: 'scheduled-meets',
      url: `${process.env.SERVER_UNSECURED_URL}/graphql/introspection`,
      skipSSLValidation: true,
    },
    includes: ['./src/**/*.graphql.ts'],
  },
};
