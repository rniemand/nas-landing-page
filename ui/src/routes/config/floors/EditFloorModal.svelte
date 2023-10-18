<script lang="ts">
	import {
		Button,
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
	import Spinner from '../../../components/common/Spinner.svelte';
	import { CoreClient, type HomeFloorDto } from '../../../nlp-api';
	import { createBlankFloorDto, validateAddFloor } from './floors';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	export let disabled: boolean = false;
	export let onFloorUpdated: () => void = () => {};
	// TODO: [COMPLETE] Replace with valid home id
	const homeID: number = 1;
	let open = false;
	let submitting: boolean = false;
	let isValid: boolean = false;
	let floor: HomeFloorDto = createBlankFloorDto(homeID);

	export const editFloor = (_floor: HomeFloorDto) => {
		floor = _floor;
		open = true;
	};

	const saveChanges = async () => {
		submitting = true;
		const response = await new CoreClient().updateFloor(floor);
		if (response.success) {
			toastSuccess('Floor Updated', `Floor "${floor.floorName}" has been added`);
			open = false;
			onFloorUpdated();
		} else {
			toastError('Error', response?.error || 'Failed to update floor');
		}
		submitting = false;
	};

	const toggle = () => (open = !open);

	$: isValid = validateAddFloor(floor);
</script>

<Modal isOpen={open} {toggle}>
	<ModalHeader>Edit Floor</ModalHeader>
	<ModalBody>
		<Spinner show={submitting} />
		{#if !submitting}
			<Form>
				<Row>
					<FormGroup>
						<Label>Floor Name</Label>
						<Input type="text" placeholder="Floor Name" bind:value={floor.floorName} />
					</FormGroup>
				</Row>
			</Form>
		{/if}
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={!isValid || submitting || disabled} on:click={saveChanges}>
			Save Changes
		</Button>
	</ModalFooter>
</Modal>
