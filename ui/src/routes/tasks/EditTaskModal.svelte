<script lang="ts">
	import {
		Modal,
		ModalHeader,
		ModalBody,
		Form,
		Row,
		Col,
		FormGroup,
		Label,
		ModalFooter,
		Button,
		Input
	} from 'sveltestrap';
	import TaskCategorySelector from './TaskCategorySelector.svelte';
	import TaskPriority from './TaskPriority.svelte';
	import TaskSubCategorySelector from './TaskSubCategorySelector.svelte';
	import { UserTasksClient, type UserTaskDto } from '../../nlp-api';
	import { createNewTask, validateTaskForAdding } from './Tasks';
	import { toastError, toastSuccess } from '../../components/ToastManager';

	let _open = false;
	let userTask: UserTaskDto = createNewTask();
	let canSave: boolean = false;
	export let onTaskEdited: () => void = () => {};

	const saveChanges = async () => {
		const response = await new UserTasksClient().updateUserTask(userTask);
		if (response.success) {
			toastSuccess('Success', 'Task has been updated');
			onTaskEdited();
			toggle();
			userTask = createNewTask();
		} else {
			toastError('Error', response.error || 'Failed to update task');
		}
	};

	const taskEntryChanged = (_userTask: UserTaskDto) => (canSave = validateTaskForAdding(_userTask));
	const toggle = () => (_open = !_open);

	export const open = (task: UserTaskDto) => {
		userTask = task;
		_open = true;
	};

	$: taskEntryChanged(userTask);
</script>

<Modal isOpen={_open} {toggle}>
	<ModalHeader {toggle}>Edit Task</ModalHeader>
	<ModalBody>
		<Form>
			<Row>
				<Col xs="9">
					<FormGroup>
						<Label for="name">Task Name</Label>
						<Input type="text" id="name" bind:value={userTask.taskName} />
					</FormGroup>
				</Col>
				<Col>
					<TaskPriority bind:value={userTask.taskPriority} />
				</Col>
			</Row>
			<Row>
				<Col>
					<FormGroup>
						<Label for="category">Category</Label>
						<TaskCategorySelector bind:value={userTask.taskCategory} showClear />
					</FormGroup>
				</Col>
				<Col>
					<FormGroup>
						<Label for="subCategory">Sub Category</Label>
						<TaskSubCategorySelector
							category={userTask.taskCategory}
							bind:value={userTask.taskSubCategory}
							showClear />
					</FormGroup>
				</Col>
			</Row>
			<FormGroup>
				<Label for="description">Task Description</Label>
				<Input type="textarea" id="description" bind:value={userTask.taskDescription} />
			</FormGroup>
		</Form>
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={!canSave} on:click={saveChanges}>Save Changes</Button>
	</ModalFooter>
</Modal>
