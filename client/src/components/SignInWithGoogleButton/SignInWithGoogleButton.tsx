import { FC, useEffect, useRef } from 'react';
import { Switch, Case, Default } from 'react-when-then';

import { env } from 'config';
import { SignInWithGoogleButtonProps, ScriptStatus, Type } from './types';
import { useScript } from './useScript';

export const SignInWithGoogleButton: FC<SignInWithGoogleButtonProps> = ({
  clientId,
  callback,
}) => {
  const scriptState = useScript(env.GOOGLE_GSI_CLIENT_URL);
  const buttonTarget = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (scriptState !== ScriptStatus.ready) return;

    window.google.accounts.id.initialize({
      client_id: clientId,
      callback: ({ credential }) => callback(credential),
    });

    if (!buttonTarget.current) return;

    window.google.accounts.id.renderButton(buttonTarget.current, {
      type: Type.standard,
    });
  }, [scriptState, buttonTarget.current]);

  return (
    <Switch condition={scriptState}>
      <Case when={ScriptStatus.ready}>
        <div ref={buttonTarget} />
      </Case>
      <Case when={ScriptStatus.idle}>
        Nenacita se google script! ({env.GOOGLE_GSI_CLIENT_URL})
      </Case>
      <Default>Ceka se na google script!</Default>
    </Switch>
  );
};
