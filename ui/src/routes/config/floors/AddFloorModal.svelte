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
	export let onFloorAdded: () => void = () => {};
	// TODO: [COMPLETE] Replace with valid home id
	const homeID: number = 1;
	let open = false;
	let submitting: boolean = false;
	let isValid: boolean = false;
	let floor: HomeFloorDto = createBlankFloorDto(homeID);

	const addFloor = async () => {
		submitting = true;
		const response = await new CoreClient().addFloor(floor);
		if (response.success) {
			toastSuccess('Floor Added', `Floor "${floor.floorName}" has been added`);
			open = false;
			onFloorAdded();
		} else {
			toastError('Error', response?.error || 'Failed to add floor');
		}
		submitting = false;
	};

	const toggle = () => {
		open = !open;
		if (open) floor = createBlankFloorDto(homeID);
	};

	$: isValid = validateAddFloor(floor);
</script>

<Button color="success" on:click={toggle}>Add Floor</Button>
<Modal isOpen={open} {toggle}>
	<ModalHeader>Add Floor</ModalHeader>
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
		<Button color="primary" disabled={!isValid || submitting || disabled} on:click={addFloor}
			>Add Floor</Button>
	</ModalFooter>
</Modal>
