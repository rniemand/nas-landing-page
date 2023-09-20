<style>
	.form-reset {
		max-width: 450px;
		width: 50%;
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
		text-decoration: none;
	}
</style>

<script lang="ts">
	import { Input, Alert, Button } from 'sveltestrap';
	import { AuthClient, SetNewPasswordRequest } from '../../nlp-api';

	let loading = true;
	let submitted = false;
	let submitting = false;
	let passwordsValid = false;
	let message = '';
	let successMessage = '';
	let email: string = '';
	let password1: string = '';
	let password2: string = '';
	const request = new SetNewPasswordRequest({ email: '', password: '' });

	const onResetPassword = async () => {
		submitting = true;
		const response = await new AuthClient().setNewPassword(request);
		const success = (response || '').toLowerCase().startsWith('success:');
		message = success ? '' : response;
		successMessage = success ? response : '';
		submitting = false;
		submitted = true;
	};

	const onValuesChanged = (_email: string, _p1: string, _p2: string) => {
		passwordsValid = false;
		successMessage = '';
		message = '';
		if (!_p1 || _p1 != _p2) return;
		if (_p1.length < 3) {
			message = 'Password is too short';
			return;
		}
		request.email = _email;
		request.password = _p1;
		passwordsValid = true;
	};

	(async () => {
		const resp = await new AuthClient().challenge(false);
		request.email = resp.email!;
		loading = false;
	})();

	$: onValuesChanged(email, password1, password2);
</script>

<div class="container">
	<div class="form-reset shadow">
		<h3 class="text-center mb-3">Reset Password</h3>
		{#if loading}<p class="text-center">Loading...</p>{/if}
		{#if !loading && !submitted}
			<Input type="email" placeholder="Email" bind:value={email} class="mb-2" />
			<Input type="password" placeholder="New Password" bind:value={password1} class="mb-2" />
			<Input type="password" placeholder="Confirm Password" bind:value={password2} class="mb-2" />
			{#if message.length > 0}<Alert color="warning" class="mt-3">{message}</Alert>{/if}
			<div class="text-end">
				<Button disabled={!passwordsValid} color="primary" on:click={onResetPassword}
					>Reset Password</Button>
			</div>
		{/if}
		{#if submitting}<p class="text-center">Submitting...</p>{/if}
		{#if submitted}
			{#if message.length > 0}<Alert color="warning">{message}</Alert>{/if}
			{#if successMessage.length > 0}<Alert color="success">{successMessage}</Alert>{/if}
			<p class="text-center">Click <a href="/login">here</a> to login.</p>
		{/if}
	</div>
</div>
