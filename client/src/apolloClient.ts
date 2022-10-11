import { ApolloClient, InMemoryCache } from '@apollo/client';
import { env } from './config/env';

export const client = new ApolloClient({
  uri: env.HOST_GRAPHQL_ENDPOINT,
  cache: new InMemoryCache(),
});
