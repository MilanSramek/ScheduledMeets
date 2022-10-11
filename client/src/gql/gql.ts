/* eslint-disable */
import * as types from './graphql';
import { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';

const documents = {
    "\n  mutation SignIn($idToken: String!) {\n    signIn(idToken: $idToken) {\n      id\n      username\n    }\n  }\n": types.SignInDocument,
};

export function graphql(source: "\n  mutation SignIn($idToken: String!) {\n    signIn(idToken: $idToken) {\n      id\n      username\n    }\n  }\n"): (typeof documents)["\n  mutation SignIn($idToken: String!) {\n    signIn(idToken: $idToken) {\n      id\n      username\n    }\n  }\n"];

export function graphql(source: string): unknown;
export function graphql(source: string) {
  return (documents as any)[source] ?? {};
}

export type DocumentType<TDocumentNode extends DocumentNode<any, any>> = TDocumentNode extends DocumentNode<  infer TType,  any>  ? TType  : never;