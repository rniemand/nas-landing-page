<style>
  .form-signin {
    max-width: 330px;
    padding: 15px;
    position: absolute;
    top: 50%;
    left: 50%;
    -ms-transform: translate(-50%, -50%);
    transform: translate(-50%, -50%);
    font-family: Roboto, 'open sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;
    font-size: 14px;
  }

  .container {
    height: 100vh;
    text-align: center;
  }

  .password {
    width: 160px;
    border: 1px solid #464646;
    padding: 6px;
    border-radius: 6px;
    margin-bottom: 10px;
  }

  .error {
    color: #f00;
  }

  .btn-login {
    border: 1px solid #b7b7b7;
    background-color: white;
    padding: 4px 8px;
    border-radius: 5px;
    font-size: 1em;
    cursor: pointer;
  }

  .btn-login:hover {
    background-color: #f7f7f7;
  }
  a { text-decoration: none; color: #1453a7; font-weight: bold; }
  a:hover { text-decoration: underline; color: red; }
  .pad-bottom { margin-bottom: 12px; }
</style>

<script lang="ts">
  import { goto } from '$app/navigation';
  import { AuthClient } from '../../nlp-api';

  const authClient = new AuthClient();
  let username = '';
  let email = '';
  let password = '';
  let passInput: HTMLInputElement | null = null;
  let wrongCredentials = false;
  let status = '';

  function triggerWrongCredentialsMessage() {
    wrongCredentials = true;
    password = '';
    if (triggerWrongCredentialsMessage._handle) window.clearTimeout(triggerWrongCredentialsMessage._handle);
    triggerWrongCredentialsMessage._handle = window.setTimeout(clearWrongCredentialsMessage, 3000);
  }

  triggerWrongCredentialsMessage._handle = <number | null>null;

  function clearWrongCredentialsMessage() {
    wrongCredentials = false;
    triggerWrongCredentialsMessage._handle = null;
  }

  (async () => {
    const resp = await authClient.challenge(false);
    username = resp.name!;
    email = resp.email!;
    if(resp.signedIn) location.href = '/';
  })();

  const triggerLogin = () => {
    if (status) return false;
    clearWrongCredentialsMessage();
    status = 'Validating credentials...';
    (async () => {
      try {
        status = 'Logging in...';
        await authClient.login(password);
        setTimeout(() => { location.reload(); }, 100);
      } catch (ex) {
        triggerWrongCredentialsMessage();
        status = '';
      }
    })();
    return false;
  };

  function triggerGoogleSelectAccount() {
    authClient.challenge(true);
  }

  $: passInput && passInput.focus();
</script>

{#if email}
  <div class="container">
    <main class="form-signin w-100 m-auto">
      <form on:submit={triggerLogin}>
        <h2 class="h3 mb-3 fw-normal">Welcome <strong>{username}</strong>!</h2>
        {#if status}
          <div>{status}</div>
        {:else}
          {#if wrongCredentials}
            <div class="error">
              Wrong password or no account with email {email}!
            </div>
          {/if}
          <div><input type="password" class="form-control password" placeholder="Password" bind:value={password} bind:this={passInput} /></div>

          <div class="pad-bottom"><a href="javascript:void 0" on:click={triggerGoogleSelectAccount}>Not <strong>{email}</strong>?</a></div>
          <div class="pad-bottom"><a href="/reset-pass">First time visitor? Forgot your password?</a></div>
          <button type="submit" class="btn-login">Sign in</button>
        {/if}
      </form>
    </main>
  </div>
{:else}
  <h1>Please wait...</h1>
{/if}

