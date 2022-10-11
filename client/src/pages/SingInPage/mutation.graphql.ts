import { gql } from '@apollo/client';

export const SIGN_IN = gql`
  mutation SignIn($idToken: String!) {
    signIn(idToken: $idToken) {
      id
    }
  }
`;
