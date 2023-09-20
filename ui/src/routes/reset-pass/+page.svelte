<style>
	.form-reset {
		max-width: 450px;
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
	.spacer {
		margin-bottom: 12px;
	}
	input {
		width: 100%;
		padding: 4px;
		border: 1px solid #c9c9c9;
	}
	input:focus {
		border: 1px solid #6284e2;
	}
	.message {
		color: rgb(167, 108, 0);
	}
	.success {
		color: rgb(0, 128, 0);
	}
	a {
		text-decoration: none;
		color: #1453a7;
		font-weight: bold;
	}
	a:hover {
		text-decoration: underline;
		color: red;
	}
</style>

<script lang="ts">
	import { AuthClient, SetNewPasswordRequest } from '../../nlp-api';

	const authClient = new AuthClient();
	let loading = true;
	let submitted = false;
	let submitting = false;
	let passwordsValid = false;
	let message = '';
	let successMessage = '';
	let email: HTMLInputElement;
	let pass1: HTMLInputElement;
	let pass2: HTMLInputElement;

	const request = new SetNewPasswordRequest({
		email: '',
		password: ''
	});

	const onPasswordChanged = () => {
		passwordsValid = false;
		successMessage = '';
		message = '';

		let pass1Value = pass1.value;
		let pass2Value = pass2.value;
		request.email = email.value;

		if (pass1Value != pass2Value) return;
		if (pass1Value.length < 3) {
			message = 'Password is too short';
			return;
		}

		request.password = pass1Value;
		passwordsValid = true;
	};

	const onResetPassword = () => {
		submitting = true;
		authClient.setNewPassword(request).then((response: string) => {
			const success = (response || '').toLowerCase().startsWith('success:');
			message = success ? '' : response;
			successMessage = success ? response : '';
			submitting = false;
			submitted = true;
		});
	};

	(async () => {
		const resp = await authClient.challenge(false);
		request.email = resp.email!;
		loading = false;
	})();
</script>

<div class="container">
	<main class="form-reset">
		<h1>Reset Password</h1>
		{#if loading}
			Loading...
		{/if}
		{#if !loading && !submitted}
			<div class="spacer">
				<input type="text" placeholder="Email" bind:this={email} on:keyup={onPasswordChanged} />
			</div>
			<div class="spacer">
				<input
					type="password"
					placeholder="New Password"
					bind:this={pass1}
					on:keyup={onPasswordChanged} />
			</div>
			<div class="spacer">
				<input
					type="password"
					placeholder="Confirm Password"
					bind:this={pass2}
					on:keyup={onPasswordChanged} />
			</div>
			{#if message.length > 0}<p class="message">{message}</p>{/if}
			<div>
				<button disabled={!passwordsValid} on:click={onResetPassword}>Reset Password</button>
			</div>
		{/if}
		{#if submitting}
			Submitting...
		{/if}
		{#if submitted}
			{#if message.length > 0}<p class="message">{message}</p>{/if}
			{#if successMessage.length > 0}<p class="success">{successMessage}</p>{/if}
			<p>Click <a href="/login">here</a> to login.</p>
		{/if}
	</main>
</div>
