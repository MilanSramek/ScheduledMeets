enum Context {
  /**
   * "Sign in with Google"
   */
  signIn = 'signin',
  /**
   * "Sign up with Google"
   */
  signUp = 'signup',
  /**
   * "Use with Google"
   */
  use = 'use',
}

export enum UxMode {
  /**
   * Performs sign-in UX flow in a pop-up window.
   */
  popup = 'popup',
  /**
   * Performs sign-in UX flow by a full page redirection.
   */
  redirect = 'redirect',
}

interface Credential {
  /**
   * Identifies the user.
   */
  id: string;
  /**
   * The password
   */
  password: string;
}

enum SelectBy {
  /**
   * Automatic sign-in of a user with an existing session who had
   * previously granted consent to share credentials.
   */
  auto = 'auto',
  /**
   * A user with an existing session who had previously granted consent
   * pressed the One Tap 'Continue as' button to share credentials.
   */
  user = 'user',
  /**
   * A user with an existing session pressed the One Tap 'Continue as'
   * button to grant consent and share credentials.
   * Applies only to Chrome v75 and higher.
   */
  user_1tap = 'user_1tap',
  /**
   * A user without an existing session pressed the One Tap 'Continue as'
   * button to select an account and then pressed the Confirm button in
   * a pop-up window to grant consent and share credentials.
   * Applies to non-Chromium based browsers.
   */
  user_2tap = 'user_2tap',
  /**
   * A user with an existing session who previously granted consent pressed
   * the Sign In With Google button and selected a Google Account from
   * 'Choose an Account' to share credentials.
   */
  btn = 'btn',
  /**
   * A user with an existing session pressed the Sign In With Google
   * button and pressed the Confirm button to grant consent and share
   * credentials.
   */
  btn_confirm = 'btn_confirm',
  /**
   * A user without an existing session who previously granted consent
   * pressed the Sign In With Google button to select a Google Account
   * and share credentials.
   */
  btn_add_session = 'btn_add_session',
  /**
   * A user without an existing session first pressed the Sign In With
   * Google button to select a Google Account and then pressed
   * the Confirm button to consent and share credentials.
   */
  btn_confirm_add_session = 'btn_confirm_add_session',
}

interface CredentialResponse {
  /**
   *  This field is the returned ID token.
   */
  credential: string;
  /**
   * This field sets how the credential is selected.
   */
  select_by: SelectBy;
}

/**
 * Data type: IdConfiguration
 * See: https://developers.google.com/identity/gsi/web/reference/js-reference#CredentialResponse
 */
interface IdConfiguration {
  /**
   * Your application's client ID
   */
  client_id: string;
  /**
   * This field determines if an ID token is automatically returned without
   * any user interaction when there's only one Google session that has
   * approved your app before. The default value is false.
   */
  auto_select?: boolean | null;
  /**
   * Handles the ID token returned from the One Tap prompt or the pop-up
   * window. This attribute is required if Google One Tap or
   * the Sign In With Google button popup UX mode is used.
   */
  callback?: (response: CredentialResponse) => void | null;
  /**
   * The URL of your login endpoint. The Sign In With Google button redirect
   * UX mode uses this attribute.
   */
  login_uri?: string | null;
  /**
   * Handles the password credential returned from the browser's native
   * credential manager.
   */
  native_callback?: (credential: Credential) => void | null;
  /**
   * This field sets whether or not to cancel the One Tap request if a user
   * clicks outside the prompt. The default value is true.
   */
  cancel_on_tap_outside?: boolean;
  /**
   * This attribute sets the DOM ID of the container element. If it's not set,
   * the One Tap prompt is displayed in the top-right corner of the window
   */
  prompt_parent_id?: string | null;
  /**
   * This field is a random string used by the ID token to prevent replay
   * attacks.
   */
  nonce?: string | null;
  /**
   * This field changes the text of the title and messages in the One Tap prompt.
   */
  context?: Context | null;
  /**
   * If you need to display One Tap in the parent domain and its subdomains,
   * pass the parent domain to this field so that a single shared-state cookie
   * is used.
   */
  state_cookie_domain?: string | null;
  /**
   * Use this field to set the UX flow used by the Sign In With Google button.
   * The default value is popup. This attribute has no impact on the OneTap UX.
   */
  ux_mode?: UxMode | null;
  /**
   * The origins that are allowed to embed the intermediate iframe.
   * One Tap will run in the intermediate iframe mode if this field presents.
   */
  allowed_parent_origin?: string | string[] | null;
  /**
   * Overrides the default intermediate iframe behavior when users manually
   * close One Tap by tapping on the 'X' button in the One Tap UI.
   * The default behavior is to remove the intermediate iframe from the DOM
   * immediately.
   *
   * The intermediate_iframe_close_callback field takes effect only in
   * intermediate iframe mode. And it has impact only to the intermediate
   * iframe, instead of the One Tap iframe. The One Tap UI is removed before
   * the callback is invoked.
   */
  intermediate_iframe_close_callback?: (...args: unknown[]) => unknown | null;
  /**
   * This field determines if the upgraded One Tap UX should be enabled
   * on browsers that support Intelligent Tracking Prevention (ITP).
   * The default value is false.
   */
  itp_support?: boolean | null;
}

export enum Type {
  /**
   * A button with text or personalized information.
   */
  standard = 'standard',
  /**
   * An icon button without text.
   */
  icon = 'icon',
}

export enum Size {
  large = 'large',
  medium = 'medium',
  small = 'small',
}

export enum Theme {
  outline = 'outline',
  filled_blue = 'filled_blue',
  filled_black = 'filled_black',
}

export enum Text {
  /**
   * The button text is “Sign in with Google”.
   */
  signinWith = 'signin_with',
  /**
   * The button text is “Sign up with Google”.
   */
  signupWith = 'signup_with',
  /**
   * The button text is “Continue with Google”
   */
  continueWith = 'continue_with',
  /**
   * The button text is “Sign in”.
   */
  signIn = 'signin',
}

export enum Shape {
  rectangular = 'rectangular',
  pill = 'pill',
  /**
   * The circle-shaped button. If used for the standard button type,
   * then it's the same as pill.
   */
  circle = 'circle',
  /**
   * The square-shaped button. If used for the standard button type,
   * then it's the same as rectangular.
   */
  square = 'square',
}

export enum LogoAlignment {
  left = 'left',
  center = 'center',
}

interface GsiButtonConfiguration {
  /**
   * The button type. The default value is standard.
   */
  type: Type;
  /**
   * The button theme. The default value is outline.
   */
  theme?: Theme | null;
  /**
   * The button size. The default value is large
   */
  size?: Size | null;
  /**
   * The button text. The default value is signin_with.
   */
  text?: Text | null;
  /**
   * The button shape. The default value is rectangular
   */
  shape?: Shape | null;
  /**
   * The alignment of the Google logo. The default value is left
   */
  logo_alignment?: LogoAlignment | null;
  /**
   * The minimum button width, in pixels. The maximum width is 400 pixels.
   */
  width?: string | null;
  /**
   * The pre-set locale of the button text. If it's not set,
   * the browser's default locale or the Google session user’s
   * preference is used. Therefore, different users might see
   * different versions of localized buttons, and possibly with
   * different sizes.
   */
  locale?: string | null;
}

interface Id {
  initialize: (config: IdConfiguration) => void;
  renderButton: (parent: HTMLElement, options: GsiButtonConfiguration) => void;
  prompt: () => void;
  cancel: () => void;
}

interface Accounts {
  id: Id;
}

interface Google {
  accounts: Accounts;
}

declare global {
  interface Window {
    google: Google;
    [key: string]: unknown;
  }
}

export interface SignInWithGoogleButtonProps {
  clientId: string;
  callback: (idToken: string) => void;
}

export enum ScriptStatus {
  idle = 'idle',
  loading = 'loading',
  ready = 'ready',
  error = 'error',
}

export enum ScriptEventType {
  load = 'load',
  error = 'error',
}
