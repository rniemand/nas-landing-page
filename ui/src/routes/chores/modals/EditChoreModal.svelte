<script lang="ts">
	import {
		Button,
		Col,
		Form,
		FormGroup,
		Input,
		Label,
		Modal,
		ModalBody,
		ModalFooter,
		ModalHeader,
		Row
	} from 'sveltestrap';
	import HomeFloorSelector from '../../../components/core/HomeFloorSelector.svelte';
	import HomeRoomSelector from '../../../components/core/HomeRoomSelector.svelte';
	import ChorePriority from '../components/ChorePriority.svelte';
	import ChoreFrequency from '../components/ChoreFrequency.svelte';
	import { createBlankChore, validateAddChore } from '../chores';
	import Spinner from '../../../components/common/Spinner.svelte';
	import { ChoreClient, CoreClient, type HomeChoreDto } from '../../../nlp-api';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	export let onChoreUpdated: () => void = () => {};
	let open = false;
	let chore: HomeChoreDto = createBlankChore();
	let loading: boolean = false;
	let submitting: boolean = false;
	let floorId: number = 0;
	let isValid: boolean = false;

	const enrichChore = async (_chore: HomeChoreDto) => {
		floorId = await new CoreClient().resolveFloorIdFromRoomId(_chore.roomId);
		loading = false;
	};

	const saveChanges = async () => {
		submitting = true;
		const response = await new ChoreClient().updateChore(chore);

		if (response.success) {
			toastSuccess('Chore Updated', `Chore "${chore.choreName}" has been updated`);
			open = false;
			onChoreUpdated();
		} else {
			toastError('Chore', response.error || 'Failed to update chore');
		}
		submitting = false;
	};

	export const editChore = (_chore: HomeChoreDto) => {
		loading = true;
		chore = _chore;
		open = true;
		enrichChore(chore);
	};

	const toggle = () => {
		open = !open;
	};

	$: isValid = validateAddChore(chore);
</script>

<Modal isOpen={open} {toggle}>
	<ModalHeader>Edit Chore</ModalHeader>
	<ModalBody>
		<Spinner show={submitting || loading} />
		{#if !submitting && !loading}
			<Form>
				<Row>
					<FormGroup>
						<Label>Chore Name</Label>
						<Input type="text" placeholder="Chore Name" bind:value={chore.choreName} />
					</FormGroup>
				</Row>
				<Row>
					<Col>
						<FormGroup>
							<Label>Floor</Label>
							<HomeFloorSelector homeId={1} bind:value={floorId} />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Room</Label>
							<HomeRoomSelector bind:floorId bind:value={chore.roomId} />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<Col>
						<FormGroup>
							<Label>Chore Points</Label>
							<Input type="number" min={0} max={10} bind:value={chore.chorePoints} />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Chore Priority</Label>
							<ChorePriority bind:value={chore.priority} />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<Col>
						<FormGroup>
							<Label>Chore Frequency</Label>
							<ChoreFrequency
								bind:modifier={chore.intervalModifier}
								bind:interval={chore.interval} />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<FormGroup>
						<Label>Chore Description</Label>
						<Input type="textarea" bind:value={chore.choreDescription} />
					</FormGroup>
				</Row>
			</Form>
		{/if}
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={!isValid || submitting} on:click={saveChanges}
			>Save Changes</Button>
	</ModalFooter>
</Modal>
