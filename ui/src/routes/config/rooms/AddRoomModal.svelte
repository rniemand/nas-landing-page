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
	import { RoomClient, type HomeRoomDto } from '../../../nlp-api';
	import { createBlankRoom, validateAddRoom } from './rooms';
	import HomeFloorSelector from '../../../components/core/HomeFloorSelector.svelte';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	export let disabled: boolean = false;
	export let floorId: number = 0;
	export let onRoomAdded: () => void = () => {};
	// TODO: [COMPLETE] make use of correct home id here
	const homeID: number = 1;
	let open = false;
	let submitting: boolean = false;
	let isValid: boolean = false;
	let room: HomeRoomDto = createBlankRoom(floorId);

	const toggle = () => {
		open = !open;
		if (open) room = createBlankRoom(floorId);
	};

	const addRoom = async () => {
		submitting = true;
		const response = await new RoomClient().addRoom(room);
		if (response.success) {
			toastSuccess('Add Room', `Room "${room.roomName}" has been added`);
			open = false;
			onRoomAdded();
		} else {
			toastError('Error', response?.error || 'Failed to add room');
		}
		submitting = false;
	};

	$: isValid = validateAddRoom(room);
</script>

<Button color="success" on:click={toggle}>Add Room</Button>
<Modal isOpen={open} {toggle}>
	<ModalHeader>Add Room</ModalHeader>
	<ModalBody>
		<Spinner show={submitting} />
		{#if !submitting}
			<Form>
				<Row>
					<FormGroup>
						<Label>Room Name</Label>
						<HomeFloorSelector homeId={homeID} bind:value={room.floorId} />
					</FormGroup>
				</Row>
				<Row>
					<FormGroup>
						<Label>Room Name</Label>
						<Input type="text" placeholder="Room Name" bind:value={room.roomName} />
					</FormGroup>
				</Row>
			</Form>
		{/if}
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={!isValid || submitting || disabled} on:click={addRoom}
			>Add Room</Button>
	</ModalFooter>
</Modal>
