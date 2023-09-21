<script lang="ts">
	import {
		Button,
		Input,
		Form,
		FormGroup,
		Modal,
		ModalBody,
		ModalFooter,
		ModalHeader,
		Label,
		Col,
		Row
	} from 'sveltestrap';
	import TaskPriority from './TaskPriority.svelte';
	import { createNewTask, validateTaskForAdding } from './Tasks';
	import TaskCategorySelector from './TaskCategorySelector.svelte';
	import type { UserTaskDto } from '../../nlp-api';
	import { toastError } from '../../components/ToastManager';

	let open = false;
	let userTask: UserTaskDto = createNewTask();
	let canAdd: boolean = false;

	const taskEntryChanged = (_userTask: UserTaskDto) => (canAdd = validateTaskForAdding(_userTask));
	const toggle = () => (open = !open);

	const addTask = async () => {
		//const response = await new UserTasksClient().addTask(userTask);
		//console.log('x', response);
		// toggle();

		toastError('Failed to add task');
	};

	$: taskEntryChanged(userTask);
</script>

<div>
	<Button color="success" on:click={toggle}>Add Task</Button>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Add Task</ModalHeader>
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
							<TaskCategorySelector bind:value={userTask.taskCategory} />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label for="subCategory">Sub Category</Label>
							<Input type="text" id="subCategory" bind:value={userTask.taskSubCategory} />
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
			<Button color="primary" disabled={!canAdd} on:click={addTask}>Add Task</Button>
		</ModalFooter>
	</Modal>
</div>
