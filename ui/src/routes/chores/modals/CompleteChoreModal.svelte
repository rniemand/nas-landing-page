<script lang="ts">
	import { Button, Modal, ModalBody, ModalFooter, ModalHeader } from 'sveltestrap';
	import type { HomeChoreDto, WhoAmIResponse } from '../../../nlp-api';
	import { createBlankChore } from '../chores';
	import UserSelector from '../../../components/core/UserSelector.svelte';
	import { onMount } from 'svelte';
	import { authContext } from '../../../utils/AppStore';

	let open = false;
	let chore: HomeChoreDto = createBlankChore();
	let userId: number = 0;

	const toggle = () => {
		open = !open;
	};

	export const completeChore = (_chore: HomeChoreDto) => {
		chore = _chore;
		open = true;
	};

	const _completeChore = () => {
		console.log('completeChore');
	};

	onMount(() => {
		return authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
			userId = _whoAmI?.userId || 0;
		});
	});
</script>

<Modal isOpen={open} {toggle}>
	<ModalHeader>Complete Chore</ModalHeader>
	<ModalBody>
		<p>Select the user to complete the chore <strong>{chore.choreName}</strong> for.</p>
		<UserSelector value={userId} />
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={userId <= 0} on:click={_completeChore}>Complete Chore</Button>
	</ModalFooter>
</Modal>
