<script lang="ts">
	import { Button, Modal, ModalBody, ModalFooter, ModalHeader } from 'sveltestrap';
	import {
		ChoreClient,
		CompleteChoreRequest,
		type HomeChoreDto,
		type WhoAmIResponse
	} from '../../../nlp-api';
	import { createBlankChore } from '../chores';
	import UserSelector from '../../../components/core/UserSelector.svelte';
	import { onMount } from 'svelte';
	import { authContext } from '../../../utils/AppStore';
	import Spinner from '../../../components/common/Spinner.svelte';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	export let onChoreCompleted: () => void = () => {};
	let open = false;
	let chore: HomeChoreDto = createBlankChore();
	let userId: number = 0;
	let submitting: boolean = false;

	const toggle = () => {
		open = !open;
	};

	export const completeChore = (_chore: HomeChoreDto) => {
		chore = _chore;
		open = true;
		submitting = false;
	};

	const _completeChore = async () => {
		const request = new CompleteChoreRequest({
			chore: chore,
			completedBy: userId
		});
		submitting = true;
		const response = await new ChoreClient().completeChore(request);
		if (response.success) {
			toastSuccess('Chore', 'Chore has been marked as completed');
			open = false;
			onChoreCompleted();
		} else {
			toastError('Error', response?.error || 'Failed to complete chore');
		}
		submitting = false;
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
		<Spinner show={submitting} />
		{#if !submitting}
			<p>Select the user to complete the chore <strong>{chore.choreName}</strong> for.</p>
			<UserSelector value={userId} />
		{/if}
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={userId <= 0 || submitting} on:click={_completeChore}
			>Complete Chore</Button>
	</ModalFooter>
</Modal>
