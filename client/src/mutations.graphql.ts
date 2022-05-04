import { gql } from '@apollo/client';

export const SIGN_IN = gql`
  mutation SingIn($idToken: String!) {
    signIn(idToken: $idToken)
  }
`;
