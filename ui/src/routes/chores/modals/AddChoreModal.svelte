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
	import { ChoreClient } from '../../../nlp-api';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	export let onChoreAdded: () => void = () => {};
	let open = false;
	let floorId: number = 0;
	let chore = createBlankChore();
	let isValid: boolean = false;
	let submitting: boolean = false;

	const toggle = () => {
		open = !open;
		if (open) chore = createBlankChore();
	};

	const addChore = async () => {
		submitting = true;
		const response = await new ChoreClient().addChore(chore);
		if (response.success) {
			toastSuccess('Chore', `Chore: "${chore.choreName}" has been added.`);
			open = false;
			onChoreAdded();
		} else {
			toastError('Chore', response.error || 'Failed to add chore');
		}
		submitting = false;
	};

	$: isValid = validateAddChore(chore);
</script>

<Button color="success" on:click={toggle}>
	<i class="bi bi-plus" />
</Button>

<Modal isOpen={open} {toggle}>
	<ModalHeader>Add Chore</ModalHeader>
	<ModalBody>
		<Spinner show={submitting} />
		{#if !submitting}
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
							<HomeFloorSelector bind:value={floorId} />
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
							<Label>Points</Label>
							<Input type="number" min={0} max={10} bind:value={chore.chorePoints} />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Priority</Label>
							<ChorePriority bind:value={chore.priority} />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<Col>
						<FormGroup>
							<Label>Frequency</Label>
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
		<Button color="primary" disabled={!isValid || submitting} on:click={addChore}>Add Chore</Button>
	</ModalFooter>
</Modal>
