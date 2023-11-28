<script lang="ts">
	import { Input, Alert, Button, Form } from 'sveltestrap';
	import { AuthClient, WhoAmIResponse } from '../../nlp-api';
	import { goto } from '$app/navigation';
	import { AppUrls } from '../../enums/AppUrls';
	import { getContext } from 'svelte';
	import type { Writable } from 'svelte/store';
	import { AppContext } from '../../enums/AppContext';

	const user = getContext<Writable<WhoAmIResponse | undefined>>(AppContext.User);
	let passInput: HTMLInputElement | null = null;
	let whoAmI: WhoAmIResponse | undefined = undefined;
	let password = '';
	let wrongCredentials = false;
	let status = '';

	function triggerWrongCredentialsMessage() {
		wrongCredentials = true;
		password = '';
		if (triggerWrongCredentialsMessage._handle)
			window.clearTimeout(triggerWrongCredentialsMessage._handle);
		triggerWrongCredentialsMessage._handle = window.setTimeout(clearWrongCredentialsMessage, 3000);
	}

	triggerWrongCredentialsMessage._handle = <number | null>null;

	const clearWrongCredentialsMessage = () => {
		wrongCredentials = false;
		triggerWrongCredentialsMessage._handle = null;
	};

	(async () => {
		whoAmI = await new AuthClient().challenge(false);
		if (whoAmI.signedIn) location.href = '/';
	})();

	const triggerLogin = async () => {
		if (status) return false;
		clearWrongCredentialsMessage();
		status = 'Validating credentials...';
		try {
			status = 'Logging in...';
			const response = await new AuthClient().login(password);
			$user = response;
			if (!response) return;
			goto(AppUrls.Home);
		} catch (ex) {
			triggerWrongCredentialsMessage();
			status = '';
		}
		return false;
	};

	const triggerGoogleSelectAccount = () => new AuthClient().challenge(true);

	$: passInput && passInput.focus();
</script>

{#if whoAmI && whoAmI.email}
	<div class="container">
		<main class="form-signin w-100 m-auto shadow">
			<Form on:submit={triggerLogin}>
				<h3 class="text-center mb-3">Welcome <strong>{whoAmI.name}</strong>!</h3>
				{#if status}
					<p class="text-center">{status}</p>
				{:else}
					{#if wrongCredentials}
						<Alert color="warning">
							Wrong password or no account with email {whoAmI.email}!
						</Alert>
					{/if}
					<Input type="password" class="mb-3" placeholder="Password" bind:value={password} />
					<a href="#!" on:click={triggerGoogleSelectAccount}
						>Not <strong>{whoAmI.email}</strong>?</a>
					<a href="/reset-pass">First time visitor? Forgot your password?</a>
					<div class="text-end">
						<Button type="submit" class="w-full" color="primary" disabled={password.length == 0}
							>Sign In</Button>
					</div>
				{/if}
			</Form>
		</main>
	</div>
{:else}
	<h3 class="text-center mb-3">Please wait...</h3>
{/if}

<style>
	.form-signin {
		max-width: 450px;
		padding: 15px;
		position: absolute;
		top: 50%;
		left: 50%;
		-ms-transform: translate(-50%, -50%);
		transform: translate(-50%, -50%);
		border: 1px solid #858484;
		border-radius: 10px;
	}
	a {
		display: block;
		text-align: center;
		text-decoration: none;
		margin-bottom: 0.4em;
	}
</style>
