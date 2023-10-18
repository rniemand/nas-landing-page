<script lang="ts">
	import { Input } from 'sveltestrap';
	import { UserClient, type UserDto } from '../../nlp-api';

	export let value: number = 0;
	let users: UserDto[] = [];
	let loading: boolean = true;

	const refreshUsers = async () => {
		loading = true;
		users = (await new UserClient().getAllUsers()) || [];
		loading = false;
	};

	refreshUsers();
</script>

<Input type="select" disabled={loading} bind:value>
	{#each users as user}
		<option value={user.userID} selected={value === user.userID}>{user.displayName}</option>
	{/each}
</Input>
